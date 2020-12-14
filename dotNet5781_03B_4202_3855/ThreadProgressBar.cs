using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_4202_3855
{
    /// <summary>
    /// this class is for all the threads - Meaning, that it sends several objects together into the thread.
    /// </summary>
    public  class ThreadProgressBar
    {
        private  object obj;
        private  Bus bus;
        private object tb;
        private int number;
        private int timeOfDriving;
        public  object Obj { get => obj; set => obj = value; }
        public  Bus Bus { get => bus; set => bus = value; }
        public object Tb { get => tb; set => tb = value; }
        public int Number { get => number; set => number = value; }
        public int TimeOfDriving { get => timeOfDriving; set => timeOfDriving = value; }
    }
}
