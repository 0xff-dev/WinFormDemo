
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SetUp.Model;
using SetUp.Common;
using System.Collections.Generic;


namespace SetUp.MyForm{
	public class FormLogin: Form{
		private Label TitleLabel = null;
		private Label AccountLabel = null;
		private Label PasswordLabel = null;
		private TextBox AccountTextBox = null;
		private TextBox PasswordTextBox = null;
		private DBManager db = null;
        private Button btn = null;


		public FormLogin(){
			db = new DBManager ();
			Text = "教师登录";
			Size = new Size (400, 300);
			TitleLabel = new Label ();
            TitleLabel.Text = "教师登录";
			AccountLabel = new Label ();
            AccountLabel.Text = "帐号";
			PasswordLabel = new Label ();
            PasswordLabel.Text = "密码";
			AccountTextBox = new TextBox ();
			PasswordTextBox = new TextBox ();

			TitleLabel.Location = new Point (150, 30);
			TitleLabel.Parent = this;
			AccountLabel.Location = new Point (80, 80);
			AccountLabel.Parent = this;
			AccountTextBox.Location = new Point (180, 80);
			AccountTextBox.Parent = this;
			PasswordLabel.Location = new Point (80, 130);
			PasswordLabel.Parent = this;
			PasswordTextBox.Location = new Point (180, 130);
            //设置登录的时候密码别样显示
            //UseSystemPasswordChar 系统的样式
            PasswordTextBox.PasswordChar = '*';
			//PasswordTextBox.Visible = false;    //密码不可见
			PasswordTextBox.Parent = this;
			btn = new Button();
            btn.Parent = this;
			btn.Text = "登录";
            btn.BackColor = System.Drawing.Color.FromName("#003DF5");
			btn.Click += OnClick;
            btn.Location = new Point(180, 180);
            CenterToScreen();
		}


		protected void OnClick(object obj, EventArgs args){
			string TeacherId = AccountTextBox.Text.ToString ();
			string Password = PasswordTextBox.Text.ToString ();
			if (login (TeacherId, Password)) {
				common.IsLogin = true;
				common.TeacherId = TeacherId;
				this.DialogResult = DialogResult.OK;db.Close();
				Close ();
			}
		}
		private bool login(string TeacherId, string Password){
			return db.IsExistUser (TeacherId, Password);
		}
	}


    public class FormSelect: Form{
        private DBManager db = null;
        private Label IdLabel = null;
        private TextBox IdTextBox = null;
        private Button GetResultBtn = null;
        private ListView lv = null;
        private Button CloseBtn = null;

        
        public FormSelect(){
            db = new DBManager();
            Text = "成绩查询";
            Size = new Size(600, 400);
            IdLabel = new Label();
            IdLabel.Size = new Size(80, 20);
            IdLabel.Text = "请输入学号";
            IdLabel.Parent = this;
            IdLabel.Location = new Point(10, 10);

            IdTextBox = new TextBox();
            //IdTextBox.Size = new Size(20, 80);
            IdTextBox.Parent = this;
            IdTextBox.Location = new Point(105, 10);

            GetResultBtn = new Button();
            //GetResultBtn.Size = new Size(20, 80);
            GetResultBtn.Text = "成绩查询";
            GetResultBtn.Parent = this;
            GetResultBtn.Location = new Point(210, 10);
            GetResultBtn.Click += OnClick;    //点击查询
            CloseBtn = new Button();
            CloseBtn.Text = "关闭";
            CloseBtn.Parent = this;
            CloseBtn.Click += OnCloseClick;
            CloseBtn.Location = new Point(300, 340);

	        lv = new ListView();
            lv.Parent = this;
            lv.Location = new Point(50, 50);
            lv.Size = new Size(500, 280);
            //想要显示标题，这个语句要加上
            lv.View = View.Details;
            lv.GridLines = true;
            //建立单个表头
            ColumnHeader CourseNameCh = new ColumnHeader();
            CourseNameCh.Text = "课程名称";
            CourseNameCh.Width = 100;
            //文字的对齐方式
            CourseNameCh.TextAlign = HorizontalAlignment.Left;
            lv.Columns.Add(CourseNameCh);

            ColumnHeader CourseScoreCh = new ColumnHeader();
            CourseScoreCh.Text = "分数";
            CourseScoreCh.Width = 100;
            CourseScoreCh.TextAlign = HorizontalAlignment.Left;
            lv.Columns.Add(CourseScoreCh);

            ColumnHeader StudentNameCh = new ColumnHeader();
            StudentNameCh.Text = "姓名";
            StudentNameCh.Width = 100;
            StudentNameCh.TextAlign = HorizontalAlignment.Left;
            lv.Columns.Add(StudentNameCh);
            CenterToScreen();
        }


        protected void OnCloseClick(object obj, EventArgs args){
            Close();
        }


        protected void OnClick(object obj, EventArgs args){
            string StudentId = IdTextBox.Text.ToString();
            Console.WriteLine(StudentId);
            DataTable dt = new DataTable();
            dt = db.SelectStudentInfo(StudentId);
            lv.Items.Clear();
            lv.BeginUpdate();
            for( int i=0; i<dt.Rows.Count; i++ ){
                ListViewItem lvi = new ListViewItem();
                lvi.Text = dt.Rows[i]["course_name"].ToString();
                lvi.SubItems.Add(dt.Rows[i]["course_score"].ToString());
                lvi.SubItems.Add(dt.Rows[i]["student_name"].ToString());
                lv.Items.Add(lvi);
            }
            lv.EndUpdate();
            Console.WriteLine(dt.Rows[0]["student_name"]);
        }
    }


    public class FormInsert: Form{
        private Label SelectCourse = null;
        private DataTable CourseDT = null;
        private ComboBox cb = null;
        private DBManager db = null;

        //在数据库中载入数据按钮
        private Button LoadData = null;
        private Button InsertData = null;    //录入提交
        private Button StatisticData = null;
        private ListView lv = null;
        private Button CloseBtn = null;

        //批量插入数据
        //private List LengthData = null;    //用于完整的提交显示一个小的dialog，提示做了多少操作
        private Label StudentNameLabel = null;
        private TextBox StudentNameBox = null;
        private Label StudentIdLabel = null;
        private TextBox StudentIdBox = null;
        private Label StudentClassLabel = null;
        private TextBox StudentClassBox = null;
        private Label CourseScoreLabel = null;
        private TextBox CourseScoreBox = null;
        //提交按钮
        private Button SubmitBtn = null;

        public FormInsert(){
            db = new DBManager();
            Text = "录入学生成绩";
            Size = new Size(850, 400);
            
            SelectCourse = new Label();
            SelectCourse.Size = new Size(60, 20);
            SelectCourse.Text = "课程";
            SelectCourse.Parent = this;
            SelectCourse.Location = new Point(10, 10);
            

            cb = new ComboBox();
            cb.Parent = this;
            cb.Location = new Point(120, 10);
            cb.Size = new Size(120, 20);

            string TeacherId = common.TeacherId;
            DataTable dt = db.SelectAllCourses(TeacherId);
            object[] objs = new object[dt.Rows.Count];
            for( int i=0; i<dt.Rows.Count; i++ ){
                objs[i] = dt.Rows[i]["course_name"].ToString();
            }
            cb.Items.AddRange(objs);
            cb.SelectionChangeCommitted += new EventHandler(OnChanged);

            LoadData = new Button();
            LoadData.Text = "载入数据";
            LoadData.Parent = this;
            LoadData.Location = new Point(250, 10);
            LoadData.Size = new Size(80, 20);
            LoadData.Click += OnClick;
            
            InsertData = new Button();
            InsertData.Text = "录入成绩提交";
            InsertData.Parent = this;
            InsertData.Location = new Point(330, 10);
            InsertData.Size = new Size(80, 20);
            //InsertData.Click += OnInsertDataClick;
            
            StatisticData = new Button();
            StatisticData.Text = "录入情况统计";
            StatisticData.Parent = this;
            StatisticData.Location = new Point(410, 10);
            StatisticData.Size = new Size(80, 20);
            //StatisticData.Click += OnStatisticDataClick;
            
            lv = new ListView();
            lv.Location = new Point(50, 50);
            ColumnHeader CourseNameCh = new ColumnHeader();
            ColumnHeader StudentIdCh = new ColumnHeader();
            ColumnHeader StudentNameCh = new ColumnHeader();
            ColumnHeader StudentClassCh = new ColumnHeader();
            ColumnHeader CourseScoreCh = new ColumnHeader();
            
            //赋值区
            CourseNameCh.Text = "课程名称";
            CourseNameCh.Width = 80;
            CourseNameCh.TextAlign = HorizontalAlignment.Left;
            lv.Columns.Add(CourseNameCh);

            StudentIdCh.Text = "学号";
            StudentIdCh.Width = 80;
            StudentIdCh.TextAlign = HorizontalAlignment.Left;
            lv.Columns.Add(StudentIdCh);

            StudentNameCh.Text = "学生姓名";
            StudentNameCh.Width = 80;
            StudentNameCh.TextAlign = HorizontalAlignment.Left;
            lv.Columns.Add(StudentNameCh);

            StudentClassCh.Text = "学生班级";
            StudentClassCh.Width = 80;
            StudentClassCh.TextAlign = HorizontalAlignment.Left;
            lv.Columns.Add(StudentClassCh);
            
            CourseScoreCh.Text = "课程分数";
            CourseNameCh.Width = 80;
            CourseScoreCh.TextAlign = HorizontalAlignment.Left;
            lv.Columns.Add(CourseScoreCh);
            lv.View = View.Details;
            lv.GridLines = true;
            lv.Size = new Size(600, 280);
            lv.Parent = this;


            StudentNameLabel = new Label();
            StudentNameLabel.Size = new Size(40, 20);
            StudentNameLabel.Text = "Name";
            StudentNameLabel.Location = new Point(30, 340);
            StudentNameLabel.Parent = this;

            StudentNameBox = new TextBox();
            StudentNameBox.Parent = this;
            StudentNameBox.Location = new Point(100, 340);
            StudentNameBox.Size = new Size(80, 20);

            StudentIdLabel = new Label();
            StudentIdLabel.Text = "ID";
            StudentIdLabel.Location = new Point(200, 340);
            StudentIdLabel.Parent = this;
            StudentIdLabel.Size = new Size(30, 20);
            
            StudentIdBox = new TextBox();
            StudentIdBox.Parent = this;
            StudentIdBox.Location = new Point(250, 340);
            StudentIdBox.Size = new Size(80, 20);

            StudentClassLabel = new Label();
            StudentClassLabel.Text = "Class";
            StudentClassLabel.Location = new Point(350, 340);
            StudentClassLabel.Parent = this;
            StudentClassLabel.Size = new Size(30, 20);

            StudentClassBox = new TextBox();
            StudentClassBox.Parent = this;
            StudentClassBox.Location = new Point(400, 340);
            StudentClassBox.Size = new Size(80, 20);

            CourseScoreLabel = new Label();
            CourseScoreLabel.Text = "Score";
            CourseScoreLabel.Parent = this;
            CourseScoreLabel.Location = new Point(500, 340);
            CourseScoreLabel.Size = new Size(40, 20);

            CourseScoreBox = new TextBox();
            CourseScoreBox.Parent = this;
            CourseScoreBox.Location = new Point(560, 340);
            CourseScoreBox.Size = new Size(80, 20);

            SubmitBtn = new Button();
            SubmitBtn.Text = "确认";
            SubmitBtn.Location = new Point(650, 340);
            SubmitBtn.Size = new Size(50, 20);
            SubmitBtn.Parent = this;
            SubmitBtn.Click += OnInsertDataClick;

            CloseBtn = new Button();
            CloseBtn.Text = "关闭";
            CloseBtn.Location = new Point(720, 340);
            CloseBtn.Size = new Size(50, 20);
            CloseBtn.Parent = this;
            CloseBtn.Click += OnClose;

            CenterToScreen();
        }


        protected void OnChanged(object obj, EventArgs args){
            ComboBox combo = (ComboBox)obj;
            common.CourseName = combo.Text;
        }


        protected void OnInsertDataClick(object obj, EventArgs args){
            string CourseName = common.CourseName.ToString();
            string StudentName = StudentNameBox.Text.ToString();
            StudentNameBox.Text = "";
            string StudentId = StudentIdBox.Text.ToString();
            StudentIdBox.Text = "";
            string StudentClass = StudentClassBox.Text.ToString();
            StudentClassBox.Text = "";
            string CourseScore = CourseScoreBox.Text.ToString();
            CourseScoreBox.Text = "";
            db.Insert(CourseName, StudentId, StudentName, StudentClass, CourseScore);
            ListViewItem item = new ListViewItem();
            item.Text = CourseName;
            item.SubItems.Add(StudentId);
            item.SubItems.Add(StudentName);
            item.SubItems.Add(StudentClass);
            item.SubItems.Add(CourseScore);
            lv.Items.Add(item);
        }


        protected void OnClose(object obj, EventArgs args){
            Close();
        }


        protected void OnClick(object obj, EventArgs args){
            //向表格插入数据
            lv.Items.Clear();
            CourseDT = new DataTable();
            CourseDT = db.CourseInfo(common.CourseName);
            Console.WriteLine(common.CourseName);
            lv.BeginUpdate();
            for( int i=0; i<CourseDT.Rows.Count; i++ ){
                ListViewItem item = new ListViewItem();
                item.Text = CourseDT.Rows[i]["course_name"].ToString();
                item.SubItems.Add(CourseDT.Rows[i]["student_id"].ToString());
                item.SubItems.Add(CourseDT.Rows[i]["student_name"].ToString());
                item.SubItems.Add(CourseDT.Rows[i]["student_class"].ToString());
                item.SubItems.Add(CourseDT.Rows[i]["course_score"].ToString());
                lv.Items.Add(item);
            }
            lv.EndUpdate();
        }
    }

    public class FormScoreAnaly: Form{
        
        private DBManager db = null;
        private Label SelectCourse = null;
        private ComboBox cb = null;
        private Button btn = null;
        private Button CloseBtn = null;

        private GroupBox OverallSituation = null;
        private Label AverageLabel = null;
        private Label Passerslabel = null;
        private Label PassingRatelabel = null;
        private TextBox AverageBox = null;
        private TextBox PassersBox = null;
        private TextBox PassingRateBox = null;

        private GroupBox PerformanceAnalysis = null;
        private Label HeightScoreLabel = null;
        private Label LowScoreLabel = null;
        private TextBox HeightScoreBox = null;
        private TextBox LowScoreBox = null;
        private Label Ge90label = null;
        private TextBox Ge90Box = null;


        public FormScoreAnaly(){
            db = new DBManager();
            Text = "成绩分析";
            Size = new Size(600, 400);

            SelectCourse = new Label();
            SelectCourse.Text = "选择课程";
            SelectCourse.Location = new Point(10, 10);
            SelectCourse.Size = new Size(80, 20);
            SelectCourse.Parent = this;

            cb = new ComboBox();
            cb.Parent = this;
            cb.Location = new Point(120, 10);
            cb.Size = new Size(120, 20);
            string TeacherId = common.TeacherId;
            DataTable dt = db.SelectAllCourses(TeacherId);
            object[] objs = new object[dt.Rows.Count];
            for( int i=0; i<dt.Rows.Count; i++ ){
                objs[i] = dt.Rows[i]["course_name"].ToString();
            }
            cb.Items.AddRange(objs);
            cb.SelectionChangeCommitted += new EventHandler(OnChanged);

            btn = new Button();
            btn.Text = "查询";
            btn.Location = new Point(260, 10);
            btn.Parent = this;
            btn.Size = new Size(60, 20);
            btn.Click += OnClick;

            //GroupBox 整体
            OverallSituation = new GroupBox();
            OverallSituation.Name = "整体情况";
            OverallSituation.Text = "整体情况";
            OverallSituation.Width = 200;
            OverallSituation.Height = 200;
            OverallSituation.Location = new Point(50, 80);
            OverallSituation.Parent = this;
            
            PerformanceAnalysis = new GroupBox();
            PerformanceAnalysis.Name = "成绩分布";
            PerformanceAnalysis.Text = "成绩分布";
            PerformanceAnalysis.Width = 200;
            PerformanceAnalysis.Height = 200;
            PerformanceAnalysis.Location = new Point(300, 80);
            OverallSituation.Parent = this;

            AverageLabel = new Label();
            AverageLabel.Text = "平均成绩";
            AverageLabel.Parent = OverallSituation;
            AverageLabel.Location = new Point(10, 40);    //在groupbox中
            AverageLabel.Size = new Size(50, 20);
            OverallSituation.Controls.Add(AverageLabel);

            AverageBox = new TextBox();
            //这个只是为了展现数据，不允许修改
            AverageBox.ReadOnly = true;
            AverageBox.Parent = OverallSituation;
            AverageBox.Location = new Point(100, 40);
            AverageBox.Size = new Size(80, 20);
            OverallSituation.Controls.Add(AverageBox);

            Passerslabel = new Label();
            Passerslabel.Text = "通过人数";
            Passerslabel.Parent = OverallSituation;
            Passerslabel.Location = new Point(10, 80);
            Passerslabel.Size = new Size(50, 20);
            OverallSituation.Controls.Add(Passerslabel);

            PassersBox = new TextBox();
            PassersBox.ReadOnly = true;
            PassersBox.Parent = OverallSituation;
            PassersBox.Location = new Point(100, 80);
            PassersBox.Size = new Size(80, 20);
            OverallSituation.Controls.Add(PassersBox);

            PassingRatelabel = new Label();
            PassingRatelabel.Text = "通过率";
            PassingRatelabel.Parent  =OverallSituation;
            PassingRatelabel.Location = new Point(10, 120);
            PassingRatelabel.Size = new Size(50, 20);
            OverallSituation.Controls.Add(PassingRatelabel);

            PassingRateBox = new TextBox();
            PassingRateBox.ReadOnly = true;
            PassingRateBox.Parent = OverallSituation;
            PassingRateBox.Location = new Point(100, 120);
            PassingRateBox.Size = new Size(80, 20);
            OverallSituation.Controls.Add(PassingRateBox);
            
            //成绩分布的部分, 90分以上的，最高分，最低分
            HeightScoreLabel = new Label();
            HeightScoreLabel.Text = "最高分";
            HeightScoreLabel.Parent = PerformanceAnalysis;
            HeightScoreLabel.Location = new Point(10, 40);
            HeightScoreLabel.Size = new Size(50, 20);
            PerformanceAnalysis.Controls.Add(HeightScoreLabel);

            HeightScoreBox = new TextBox();
            HeightScoreBox.ReadOnly = true;
            HeightScoreBox.Parent = PerformanceAnalysis;
            HeightScoreBox.Location = new Point(100, 40);
            HeightScoreBox.Size = new Size(80, 20);
            PerformanceAnalysis.Controls.Add(HeightScoreBox);

            LowScoreLabel = new Label();
            LowScoreLabel.Text = "最低分";
            LowScoreLabel.Parent = PerformanceAnalysis;
            LowScoreLabel.Location = new Point(10, 80);
            LowScoreLabel.Size = new Size(50, 20);
            PerformanceAnalysis.Controls.Add(LowScoreLabel);

            LowScoreBox = new TextBox();
            LowScoreBox.ReadOnly = true;
            LowScoreBox.Parent = PerformanceAnalysis;
            LowScoreBox.Location = new Point(100, 80);
            LowScoreBox.Size = new Size(80, 20);
            PerformanceAnalysis.Controls.Add(LowScoreBox);

            Ge90label = new Label();
            Ge90label.Text = ">=90";
            Ge90label.Parent = PerformanceAnalysis;
            Ge90label.Location = new Point(10, 120);
            Ge90label.Size = new Size(50, 20);
            PerformanceAnalysis.Controls.Add(Ge90label);

            Ge90Box = new TextBox();
            Ge90Box.ReadOnly = true;
            Ge90Box.Parent = PerformanceAnalysis;
            Ge90Box.Location = new Point(100, 120);
            Ge90Box.Size = new Size(80, 20);
            PerformanceAnalysis.Controls.Add(Ge90Box);
            this.Controls.Add(OverallSituation);
            this.Controls.Add(PerformanceAnalysis);

            CloseBtn = new Button();
            CloseBtn.Text = "关闭";
            CloseBtn.Parent = this;
            CloseBtn.Location = new Point(400, 340);
            CloseBtn.Size = new Size(50, 20);
            CloseBtn.Click += OnClose;
            CenterToScreen();
        }


        protected void OnChanged(object obj, EventArgs args){
            ComboBox tmp = (ComboBox)obj;
            common.CourseName = tmp.Text;
        }

        protected void OnClose(object obj, EventArgs args){
            Close();
        }


        protected void OnClick(object obj, EventArgs args){
            float Scores = 0;
            int Passers = 0;
            float PassRate = 0;
            int Ge90ers = 0;
            float MaxScore = 0, MinScore = 100;
            DataTable dt = db.CourseInfo(common.CourseName);
            int humans = dt.Rows.Count;
            for( int i=0; i<humans; i++ ){
                string score = dt.Rows[i]["course_score"].ToString();
                float tmp = float.Parse(score);
                if( tmp >= 90 )
                    Ge90ers += 1;
                if( tmp >= 60 )
                    Passers+=1;
                if( tmp>MaxScore )
                    MaxScore = tmp;
                if( tmp<MinScore )
                    MinScore = tmp;
                Scores += float.Parse(score);
            }
            AverageBox.Text = Convert.ToString(Scores/humans);
            PassersBox.Text = Convert.ToString(Passers);
            PassRate = Passers/humans*100;
            PassingRateBox.Text = Convert.ToString(PassRate)+"%";
            HeightScoreBox.Text = Convert.ToString(MaxScore);
            LowScoreBox.Text = Convert.ToString(MinScore);
            Ge90Box.Text = Convert.ToString(Ge90ers);
        }
    }
}
