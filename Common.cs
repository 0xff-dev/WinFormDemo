
using System;

namespace SetUp.Common{
	public static class common{
		private static bool isLogin = false;
		private static string teacherId = null;
        private static string courseName = null;
		public static bool IsLogin{
			get{
				return isLogin;
			}
			set{
				isLogin = value;
			}
		}
		public static string TeacherId{
			get {
				return teacherId;
			}
			set {
				teacherId=value;
			}
		}
        public static string CourseName{
            get{
                return courseName;
            }
            set{
                courseName = value;
            }
        }
	}
}
