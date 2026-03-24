using Comp586GroupProject.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Comp586GroupProject.Interfaces
{
    public interface IPatientInterface
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();
        Task<Patient> GetPatientByIdAsync(int id);
        Task<Patient> AddPatientAsync(Patient patient);
        Task<Patient> UpdatePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(int id);
    }
}