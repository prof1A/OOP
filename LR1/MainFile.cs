﻿using BaguetFactory;
using System;
using System.Windows.Forms;

class MainClass
{
    [STAThread]
    static void Main(string[] args)
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new BaguetForm());
    }
}