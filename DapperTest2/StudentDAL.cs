using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using MySql.Data.MySqlClient;

namespace DapperTest
{
    public class StudentDAL
    {
        public string connStr = ConfigurationManager.ConnectionStrings["connstr"].ToString();
        /// <summary>
        /// 查询返回列表
        /// </summary>
        /// <returns></returns>
        public List<Student> QueryList()
        {
            using (var db = new MySqlConnection(connStr))
            {
                return db.Query<Student>("select * from student").ToList();
            }
        }
        /// <summary>
        /// 查询返回实体
        /// </summary>
        /// <param name="stuId"></param>
        /// <returns></returns>
        public Student QueryList(int stuId)
        {
            using (var db = new MySqlConnection(connStr))
            {
                return db.Query<Student>("select * from student where StuID=@stuId", new { stuId }).FirstOrDefault();
            }
        }
        /// <summary>
        /// 插入实体类
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public int Insert(Student student)
        {
            using (var db = new MySqlConnection(connStr))
            {
                string sqlStr = "insert into student (StuID,StuName,Birthday) values(@StuId,@StuName,@Birthday)";
                return db.Execute(sqlStr, new { StuId = student.StuID, StuName = student.StuName, Birthday = student.Birthday });
            }
        }
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="lstStudents"></param>
        /// <returns></returns>
        public int BatchInsert(List<Student> lstStudents)
        {
            using (var db = new MySqlConnection(connStr))
            {
                string sqlStr = "insert into student (StuID,StuName,Birthday) values(@StuId,@StuName,@Birthday)";
                return db.Execute(sqlStr, lstStudents);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        public int Delete(Student stu)
        {
            using (var db = new MySqlConnection(connStr))
            {
                string sqlStr = "delete from student where StuID=@StuId";
                return db.Execute(sqlStr, stu);
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="stuId"></param>
        /// <param name="stuName"></param>
        /// <param name="birthday"></param>
        /// <returns></returns>
        public int Update(int stuId, string stuName, DateTime birthday)
        {
            using (var db = new MySqlConnection(connStr))
            {
                string sqlStr = "update student set StuName=@StuName,Birthday=@Birthday where StuID=@StuId";
                return db.Execute(sqlStr, new { StuId = stuId, StuName = stuName, Birthday = birthday });
            }
        }
        /// <summary>
        /// 事务
        /// </summary>
        public void Transaction()
        {
            using (var db = new MySqlConnection(connStr))
            {
                db.Open();
                IDbTransaction trans = db.BeginTransaction();
                try
                {
                    int row = db.Execute(@"update student set StuName='www.lanhusoft.com' where id=@id", new { id = 3 }, trans);
                    row += db.Execute("delete from student where id=@id", new { id = 32 }, trans);
                    for (int i = 39; i < 50; i++)
                    {
                        db.Execute(@"insert student(id, StuID, StuName) values (@id, @StuID, @StuName)", new { id = i,StuID=12+i, StuName = "www.lanhusoft.com/" + i });
                    }
                    trans.Commit();
                    db.Close();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                }

            }
        }
    }
}