using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DapperTest
{
    public class Student
    {
        public int ID { get; set; }
        public int StuID { get; set; }
        public string StuName { get; set; }
        public DateTime Birthday { get; set; }

        public string ToString()
        {
            StringBuilder sb=new StringBuilder();
            sb.Append("ID:").Append(this.ID);
            sb.Append(" 学号:").Append(this.StuID);
            sb.Append(" 姓名:").Append(this.StuName);
            sb.Append(" 出生日期:").Append(this.Birthday);
            return sb.ToString();
        }
    }
}