using System.Linq;

namespace Be3.Health.Domain.Validators
{
    public static class CpfValidator
    {
        public static bool IsValid(string? cpfRaw)
        {
            if (string.IsNullOrWhiteSpace(cpfRaw))
                return false;

            // Só dígitos
            var cpf = new string(cpfRaw.Where(char.IsDigit).ToArray());

            // Tem que ter 11 dígitos
            if (cpf.Length != 11)
                return false;

            // Rejeita CPFs com todos os dígitos iguais (tipo 11111111111)
            if (new string(cpf[0], 11) == cpf)
                return false;

            int CalcularDigito(int length)
            {
                var soma = 0;
                for (int i = 0; i < length; i++)
                {
                    var num = cpf[i] - '0';
                    soma += num * (length + 1 - i);
                }

                var resto = soma % 11;
                return resto < 2 ? 0 : 11 - resto;
            }

            var d1 = CalcularDigito(9);
            var d2 = CalcularDigito(10);

            return cpf[9] - '0' == d1 &&
                   cpf[10] - '0' == d2;
        }
    }
}
