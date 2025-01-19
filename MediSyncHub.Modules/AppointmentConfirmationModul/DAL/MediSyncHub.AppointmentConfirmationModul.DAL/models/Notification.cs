using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediSyncHub.SharedKernel.Data;

namespace MediSyncHub.AppointmentConfirmationModule.DAL.models
{
    public class Notification:BaseEntity<Guid>
    {
      
        public Guid AppointmentId { get; set; }
        public Guid PatientId { get; set; }
        public required string PatientName { get; set; }
        public DateTime BookedAt { get; set; }
        public virtual  Appointment Appointment { get; set; }
        
        private Notification()
        {
        }
        
        public static Notification Create(
            Guid appointmentId,
            Guid slotId,
            Guid patientId,
            string patientName)
        {
            var notification = new Notification
            {
                Id = Guid.NewGuid(),
                AppointmentId = appointmentId,
                PatientId = patientId,
                PatientName = patientName,
                BookedAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow
            };
            return notification;
        }
        
    }
}
