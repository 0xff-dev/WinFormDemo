

using MySql.Data.MySqlClient;
using System;
using System.Text;
using System.Linq;
using System.Data;
using System.Xml;
using System.Security.Cryptography;    //用于对密码的额md5的加密, md5是不可逆的!


namespace SetUp.Model{

	/// <summary>
	/// 作用: 链接数据库
	/// 作者: stevenshaung
	/// date: 2018.4.28
	/// </summary>
	public class DBManager{
		private MySqlConnection conn = null;
		private MySqlCommand cmd = null;
		private string ConnSql = @"Data Source=localhost;port=3306;User Id=root;
		password=*******;Database=test";


		public DBManager(){
			try{
				conn = new MySqlConnection (this.ConnSql);
				conn.Open();
			}
			catch(MySqlException ex){
				Console.WriteLine (ex.ToString ());
			}
		}

		
        /// <summary>
		/// Inserts the teacher info.
		/// </summary>
		/// <returns><c>true</c>, if teacher info was inserted, <c>false</c> otherwise.</returns>
		/// <param name="UserName">User name.</param>
		/// <param name="Password">Password.</param>
		public bool InsertTeacherInfo(string UserName, string Password){

			byte[] buffers = Encoding.Default.GetBytes (Password);
			MD5 md5 = MD5.Create ();
			byte[] NewBuffers = md5.ComputeHash (buffers);
			string passwd = null;
			for (int i = 0; i < NewBuffers.Length; i++)
				passwd += NewBuffers [i].ToString ("x2");
			string InsertTeacherInfoSql = string.Format (@"insert into Teacher(teacher_id,
			password) values('{0}','{1}')", UserName, passwd);
			cmd = new MySqlCommand (InsertTeacherInfoSql, conn);
			int RowCount = cmd.ExecuteNonQuery ();
			if (RowCount > 0)
				return true;
			return false;
		}


		/// <summary>
		/// Determines whether this instance is exist user the specified UserName Password.
		/// </summary>
		/// <returns><c>true</c> if this instance is exist user the specified UserName Password; otherwise, <c>false</c>.</returns>
		/// <param name="UserName">User name.</param>
		/// <param name="Password">Password.</param>
		public bool IsExistUser(string UserName, string Password){
			//字符串解析为字节数组
			byte[] buffer = Encoding.Default.GetBytes (Password);
			MD5 md5 = MD5.Create ();
			byte[] NewBuffer = md5.ComputeHash (buffer);
			string passwd = null;
			for (int i = 0; i < NewBuffer.Length; i++) {
				passwd += NewBuffer [i].ToString ("x2");
			}
			string QuerySql = string.Format (@"select count(*) from Teacher 
			where teacher_id='{0}' and password='{1}'", UserName, passwd);
			cmd = new MySqlCommand (QuerySql, conn);
			object result = cmd.ExecuteScalar ();
			Console.WriteLine("object result:{0}", result.ToString());
			if (int.Parse(result.ToString()) == 1)
				return true;
			return false;
		}


		/// <summary>
		/// Insert the specified CourseName, StudentId, StudentName, StudentClass and CourseScore.
		/// </summary>
		/// <param name="CourseName">Course name.</param>
		/// <param name="StudentId">Student identifier.</param>
		/// <param name="StudentName">Student name.</param>
		/// <param name="StudentClass">Student class.</param>
		/// <param name="CourseScore">Course score.</param>
		public void Insert(string CourseName, string StudentId, string StudentName,
			string StudentClass, string CourseScore){
			string InsertScoreInfoSql = string.Format (@"insert into StudentInfo(course_name,
			student_id,student_name,student_class,course_score) values('{0}','{1}','{2}',
			'{3}','{4}')",CourseName, StudentId, StudentName, StudentClass, CourseScore);
			cmd = new MySqlCommand (InsertScoreInfoSql, conn);
			cmd.ExecuteNonQuery ();
			cmd = null;
		}


        public DataTable SelectAllCourses(string TeacherId){
            string SelectAllCoursesSql = string.Format(@"select course_name from TeacherCourses where 
                                                       teacher_id='{0}'", TeacherId);
            DataTable dt = new DataTable();
            MySqlDataAdapter mda = new MySqlDataAdapter(SelectAllCoursesSql, conn);
            mda.Fill(dt);
            return dt;
        }


		/// <summary>
		/// Selects the student info.
		/// </summary>
		/// <returns>The student info.</returns>
		/// <param name="StudentId">Student identifier.</param>
		public DataTable SelectStudentInfo(string StudentId){
			string SelectStudentInfoSql = string.Format(@"select course_name,
                                                        course_score, student_name from StudentInfo where 
			                                            student_id='{0}'", StudentId);
			DataTable dt = new DataTable ();
			MySqlDataAdapter mda = new MySqlDataAdapter (SelectStudentInfoSql, conn);
			mda.Fill (dt);
			return dt;
		}


		/// <summary>
		/// Courses the info.
		/// </summary>
		/// <returns>The info.</returns>
		/// <param name="CourseName">Course name.</param>
		public DataTable CourseInfo(string CourseName){
			string CourseInfoSql = string.Format (@"select course_name, student_id, student_name,
                                                  student_class, course_score from StudentInfo where 
			                                      course_name='{0}'", CourseName);
			DataTable dt = new DataTable ();
			MySqlDataAdapter mda = new MySqlDataAdapter (CourseInfoSql, conn);
			mda.Fill (dt);
			return dt;
		}


		/// <summary>
		/// Close this instance.
		/// </summary>
		public void Close(){
			conn.Close ();
		}
		/*
        public static void Main(){
            DBManager db = new DBManager();
            db.InsertTeacherInfo("xiaoliu", "000000");
            db.Close();
        }
        */
	}
}
