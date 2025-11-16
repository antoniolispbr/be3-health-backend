namespace Be3.Health.Application.Dtos
{
    public class PacienteCreateDto
    {
        public string Nome { get; set; } = null!;
        public string Sobrenome { get; set; } = null!;
        public DateOnly DataNascimento { get; set; }
        public byte Genero { get; set; }      // 0..3

        public string? CPF { get; set; }
        public string RG { get; set; } = null!;
        public string UFRg { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string? Celular { get; set; }
        public string? TelefoneFixo { get; set; }

        public int ConvenioId { get; set; }
        public string? NumeroCarteirinha { get; set; }
        public byte? ValidadeCarteirinhaMes { get; set; }
        public short? ValidadeCarteirinhaAno { get; set; }
    }
}
