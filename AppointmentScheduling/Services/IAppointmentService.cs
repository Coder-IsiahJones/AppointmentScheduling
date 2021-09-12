using AppointmentScheduling.Models.ViewModels;
using System.Collections.Generic;

namespace AppointmentScheduling.Services
{
    public interface IAppointmentService
    {
        public List<DoctorViewModel> GetDoctorList();

        public List<PatientViewModel> GetPatientList();
    }
}