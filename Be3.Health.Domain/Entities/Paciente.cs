using Be3.Health.Domain.Entities;
using Be3.Health.Domain.Enums;

public class Paciente
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Sobrenome { get; set; } = null!;
    public DateOnly DataNascimento { get; set; }
    public Genero Genero { get; set; }

    public string CPF { get; set; } = null!;
    public string RG { get; set; } = null!;
    public string UFRg { get; set; } = null!;

    public string Email { get; set; } = null!;
    public string Celular { get; set; } = null!;
    public string TelefoneFixo { get; set; } = null!;

    public int ConvenioId { get; set; }
    public Convenio Convenio { get; set; } = null!;

    // 👉 campos exigidos no teste
    public string? NumeroCarteirinha { get; set; }
    public byte? ValidadeCarteirinhaMes { get; set; }
    public short? ValidadeCarteirinhaAno { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CriadoEm { get; set; }
    public DateTime? AtualizadoEm { get; set; }
}
