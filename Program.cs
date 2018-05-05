
using System;
using System.Windows.Forms;
using SetUp.Main;
using SetUp.MyForm;
using SetUp.Common;

class Program{
	public static void Main(string[] args){
		FormLogin fl = new FormLogin ();
		fl.ShowDialog ();
		if (fl.DialogResult == DialogResult.OK) {
			Application.Run (new Mainwindow ());
		}
	}
}
