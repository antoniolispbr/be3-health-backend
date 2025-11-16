using System;
using System.Collections.Generic;
using Be3.Health.Domain.Validators;
using Be3.Health.Application.Dtos;
using Be3.Health.Application.Interfaces;
using Be3.Health.Domain.Entities;
using Be3.Health.Domain.Enums;
using Be3.Health.Domain.Exceptions;
using Be3.Health.Domain.Interfaces;

namespace Be3.Health.Application.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _repository;

        public PacienteService(IPacienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PacienteResponseDto>> ListarAsync()
        {
            var pacientes = await _repository.ListarAsync();

            // Exibir apenas pacientes ativos (exclusão lógica)
            return pacientes
                .Where(p => p.IsActive)
                .Select(MapToResponse)
                .ToList();
        }

        public async Task<PacienteResponseDto?> ObterPorIdAsync(Guid id)
        {
            var paciente = await _repository.ObterPorIdAsync(id);
            return paciente == null ? null : MapToResponse(paciente);
        }

        public async Task<PacienteResponseDto> CriarAsync(PacienteCreateDto dto)
        {


            var hoje = DateOnly.FromDateTime(DateTime.Today);
            if (dto.DataNascimento > hoje)
                throw new BusinessException("A data de nascimento não pode ser futura.");

            if (string.IsNullOrWhiteSpace(dto.Celular) &&
                string.IsNullOrWhiteSpace(dto.TelefoneFixo))
            {
                throw new BusinessException("Informe pelo menos um telefone (celular ou telefone fixo).");
            }

            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
                throw new BusinessException("E-mail inválido.");


            if (!string.IsNullOrWhiteSpace(dto.CPF))
            {
                if (!CpfValidator.IsValid(dto.CPF))
                    throw new BusinessException("CPF inválido.");

                var cpfJaExiste = await _repository.ExisteCpfAsync(dto.CPF);
                if (cpfJaExiste)
                    throw new BusinessException("Já existe um paciente cadastrado com este CPF.");
            }

            var agora = DateTime.UtcNow;

            var paciente = new Paciente
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                Sobrenome = dto.Sobrenome,
                DataNascimento = dto.DataNascimento,

                // Conversão correta do Enum
                Genero = (Genero)dto.Genero,

                CPF = dto.CPF != null ? LimparCpf(dto.CPF) : null!,
                RG = dto.RG,
                UFRg = dto.UFRg,
                Email = dto.Email,
                Celular = dto.Celular,
                TelefoneFixo = dto.TelefoneFixo,
                ConvenioId = dto.ConvenioId,

                // Campos da carteirinha
                NumeroCarteirinha = dto.NumeroCarteirinha,
                ValidadeCarteirinhaMes = dto.ValidadeCarteirinhaMes,
                ValidadeCarteirinhaAno = dto.ValidadeCarteirinhaAno,

                // Novo cadastro sempre ativo
                IsActive = true,

                // Controle de auditoria
                CriadoEm = agora,
                AtualizadoEm = agora
            };

            await _repository.AdicionarAsync(paciente);

            return MapToResponse(paciente);
        }

        public async Task<PacienteResponseDto?> AtualizarAsync(Guid id, PacienteUpdateDto dto)
        {
            var paciente = await _repository.ObterPorIdAsync(id);
            if (paciente == null)
                return null;

            var hoje = DateOnly.FromDateTime(DateTime.Today);
            if (dto.DataNascimento > hoje)
                throw new BusinessException("A data de nascimento não pode ser futura.");

            if (string.IsNullOrWhiteSpace(dto.Celular) &&
                string.IsNullOrWhiteSpace(dto.TelefoneFixo))
            {
                throw new BusinessException("Informe pelo menos um telefone (celular ou telefone fixo).");
            }

            if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
                throw new BusinessException("E-mail inválido.");

            if (!string.IsNullOrWhiteSpace(dto.CPF))
            {
                if (!CpfValido(dto.CPF))
                    throw new BusinessException("CPF inválido.");

                var cpfJaExiste = await _repository.ExisteCpfAsync(LimparCpf(dto.CPF), id);
                if (cpfJaExiste)
                    throw new BusinessException("Já existe outro paciente cadastrado com este CPF.");
            }

            paciente.Nome = dto.Nome;
            paciente.Sobrenome = dto.Sobrenome;
            paciente.DataNascimento = dto.DataNascimento;

            paciente.Genero = (Genero)dto.Genero;

            paciente.CPF = dto.CPF != null ? LimparCpf(dto.CPF) : paciente.CPF;
            paciente.RG = dto.RG;
            paciente.UFRg = dto.UFRg;
            paciente.Email = dto.Email;
            paciente.Celular = dto.Celular;
            paciente.TelefoneFixo = dto.TelefoneFixo;
            paciente.ConvenioId = dto.ConvenioId;

            // Atualização opcional do IsActive via PUT
            if (dto.IsActive.HasValue)
                paciente.IsActive = dto.IsActive.Value;

            // Campos da carteirinha
            paciente.NumeroCarteirinha = dto.NumeroCarteirinha;
            paciente.ValidadeCarteirinhaMes = dto.ValidadeCarteirinhaMes;
            paciente.ValidadeCarteirinhaAno = dto.ValidadeCarteirinhaAno;

            paciente.AtualizadoEm = DateTime.UtcNow;

            await _repository.AtualizarAsync(paciente);

            return MapToResponse(paciente);
        }

        public async Task<bool> RemoverAsync(Guid id)
        {
            var paciente = await _repository.ObterPorIdAsync(id);
            if (paciente == null)
                return false;

            // Exclusão lógica: marca como inativo
            paciente.IsActive = false;
            paciente.AtualizadoEm = DateTime.UtcNow;

            await _repository.AtualizarAsync(paciente);
            return true;
        }

        // ----------------- Helpers internos -----------------

        private static PacienteResponseDto MapToResponse(Paciente p)
        {
            return new PacienteResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Sobrenome = p.Sobrenome,
                DataNascimento = p.DataNascimento,
                Genero = (byte)p.Genero,

                CPF = p.CPF,
                RG = p.RG,
                UFRg = p.UFRg,
                Email = p.Email,
                Celular = p.Celular,
                TelefoneFixo = p.TelefoneFixo,

                ConvenioId = p.ConvenioId,
                NumeroCarteirinha = p.NumeroCarteirinha,
                ValidadeCarteirinhaMes = p.ValidadeCarteirinhaMes,
                ValidadeCarteirinhaAno = p.ValidadeCarteirinhaAno,

                IsActive = p.IsActive,
                CriadoEm = p.CriadoEm,
                AtualizadoEm = p.AtualizadoEm
            };
        }

        private static string LimparCpf(string cpf)
        {
            return new string(cpf.Where(char.IsDigit).ToArray());
        }

        private static bool CpfValido(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            var digits = LimparCpf(cpf);

            if (digits.Length != 11)
                return false;

            // rejeita CPFs com todos os dígitos iguais
            if (new string(digits[0], 11) == digits)
                return false;

            // cálculo do primeiro dígito verificador
            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += (digits[i] - '0') * (10 - i);
            }

            int resto = soma % 11;
            int dv1 = resto < 2 ? 0 : 11 - resto;

            // cálculo do segundo dígito verificador
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += (digits[i] - '0') * (11 - i);
            }

            resto = soma % 11;
            int dv2 = resto < 2 ? 0 : 11 - resto;

            return digits[9] - '0' == dv1 && digits[10] - '0' == dv2;
        }
    }
}
