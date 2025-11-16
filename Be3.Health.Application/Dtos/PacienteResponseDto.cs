using System;

namespace Be3.Health.Application.Dtos
{
    public class PacienteResponseDto
    {
        public Guid Id { get; set; }

        // Dados básicos
        public string Nome { get; set; } = null!;
        public string Sobrenome { get; set; } = null!;
        public DateOnly DataNascimento { get; set; }
        public byte Genero { get; set; }   // 0 = NaoInformado, 1 = Masc, 2 = Fem, 3 = Outro

        // Documentos
        public string? CPF { get; set; }
        public string RG { get; set; } = null!;
        public string UFRg { get; set; } = null!;

        // Contato
        public string Email { get; set; } = null!;
        public string? Celular { get; set; }
        public string? TelefoneFixo { get; set; }

        // Convênio
        public int ConvenioId { get; set; }
        public string? NumeroCarteirinha { get; set; }
        public byte? ValidadeCarteirinhaMes { get; set; }
        public short? ValidadeCarteirinhaAno { get; set; }

        // Status / auditoria
        public bool IsActive { get; set; }
        public DateTime CriadoEm { get; set; }
        public DateTime? AtualizadoEm { get; set; }
    }
}
