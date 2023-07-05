using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sln_TJSJJ.Model
{
    class UUT_RESULT
    {
        public UUT_RESULT()
        {
            this.ID = Guid.NewGuid().ToString();
        }
        public string ID { get; set; }
        public string STATION_ID { get;set; }
        public string BATCH_SERIAL_NUMBER { get;  set; }
        public int TEST_SOCKET_INDEX { get; set; }
        public string UUT_SERIAL_NUMBER { get; set; }
        public string USER_LOGIN_NAME { get;  set; }
        public DateTime START_DATE_TIME { get; set; }
        public float EXECUTION_TIME { get; set; }
        public string UUT_STATUS { get; set; }
        public int UUT_ERROR_CODE { get;  set; }
        public string UUT_ERROR_MESSAGE { get; set; }
    }
}
