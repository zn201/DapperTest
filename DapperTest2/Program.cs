using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperTest;
using MySql.Data.MySqlClient;

namespace DapperTest2
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentDAL stuDal = new StudentDAL();


            var lstStu = stuDal.QueryList();//查询返回列表
            Console.WriteLine("查询返回列表如下：");
            foreach (var stu in lstStu)
            {
                Console.WriteLine(stu.ToString());
            }


            Student stuInfo = new Student();
            stuInfo.StuID = 112;
            Console.WriteLine("查询返回实体如下：");
            var resultQuery = stuDal.QueryList(stuInfo.StuID);//查询返回实体
            Console.WriteLine(resultQuery.ToString());


            Student stuInfo1 = new Student();
            stuInfo1.StuID = 115;
            stuInfo1.StuName = "王尼玛";
            stuInfo1.Birthday = Convert.ToDateTime("1998-02-24");
            int resultInsert = stuDal.Insert(stuInfo1);//插入单条数据

            List<Student> lsStudents = new List<Student>();
            lsStudents.Add(stuInfo1);
            Student stuInfo2 = new Student();
            stuInfo2.StuID = 116;
            stuInfo2.StuName = "许文强";
            stuInfo2.Birthday = Convert.ToDateTime("1995-12-24");
            lsStudents.Add(stuInfo2);
            int resultBatchInsert = stuDal.BatchInsert(lsStudents);//批量插入数据

            int resultDelete = stuDal.Delete(stuInfo1);//删除

            int resultUpdate = stuDal.Update(115, "傻根", Convert.ToDateTime("1993-06-22"));//更新

            stuDal.Transaction();//事务


            Console.ReadLine();
        }
    }
}
