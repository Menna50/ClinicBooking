using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicBooking.DAL.Data.Enums
{
    public enum AppointmentStatus
    {
    
        Scheduled = 0,   // تم تحديد الموعد
        Completed = 1,   // تم الحضور والإنهاء
        Cancelled = 2,   // أُلغي من الطرفين أو من أحدهم
        NoShow = 3       // المريض لم يحضر
    
}
}
