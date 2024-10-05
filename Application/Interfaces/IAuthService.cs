namespace HealthMed.Application.Interfaces
{
    public interface IAuthService
    {
        Task<string> LoginMedicoAsync(string email, string senha);
        Task<string> LoginPacienteAsync(string email, string senha);
    }
}
