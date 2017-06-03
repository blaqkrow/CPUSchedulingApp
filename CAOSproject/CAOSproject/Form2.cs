using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CAOSproject
{
    public partial class Form2 : Form
    {
        Form1 originalForm;
        public Form2(Form1 schduleForm)
        {
            originalForm = schduleForm;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            st0TB.Text = Convert.ToString(originalForm.process[0].startingTime);
            st1TB.Text = Convert.ToString(originalForm.process[1].startingTime);
            st2TB.Text = Convert.ToString(originalForm.process[2].startingTime);
            st3TB.Text = Convert.ToString(originalForm.process[3].startingTime);
            st4TB.Text = Convert.ToString(originalForm.process[4].startingTime);

            et0TB.Text = Convert.ToString(originalForm.process[0].completionTime);
            et1TB.Text = Convert.ToString(originalForm.process[1].completionTime);
            et2TB.Text = Convert.ToString(originalForm.process[2].completionTime);
            et3TB.Text = Convert.ToString(originalForm.process[3].completionTime);
            et4TB.Text = Convert.ToString(originalForm.process[4].completionTime);

            wt0TB.Text = Convert.ToString(originalForm.process[0].waitingTime);
            wt1TB.Text = Convert.ToString(originalForm.process[1].waitingTime);
            wt2TB.Text = Convert.ToString(originalForm.process[2].waitingTime);
            wt3TB.Text = Convert.ToString(originalForm.process[3].waitingTime);
            wt4TB.Text = Convert.ToString(originalForm.process[4].waitingTime);

            tt0TB.Text = Convert.ToString(originalForm.process[0].turnaroundTime);
            tt1TB.Text = Convert.ToString(originalForm.process[1].turnaroundTime);
            tt2TB.Text = Convert.ToString(originalForm.process[2].turnaroundTime);
            tt3TB.Text = Convert.ToString(originalForm.process[3].turnaroundTime);
            tt4TB.Text = Convert.ToString(originalForm.process[4].turnaroundTime);

            AvgTTlabel.Text = Convert.ToString(originalForm.AvgTT);
            AvgWTlabel.Text = Convert.ToString(originalForm.AvgWT);

            g0.Text = Convert.ToString(originalForm.TaskSequence[0]);
            g1.Text = Convert.ToString(originalForm.TaskSequence[1]);
            g2.Text = Convert.ToString(originalForm.TaskSequence[2]);
            g3.Text = Convert.ToString(originalForm.TaskSequence[3]);
            g4.Text = Convert.ToString(originalForm.TaskSequence[4]);
            g5.Text = Convert.ToString(originalForm.TaskSequence[5]);
            g6.Text = Convert.ToString(originalForm.TaskSequence[6]);
            g7.Text = Convert.ToString(originalForm.TaskSequence[7]);
            g8.Text = Convert.ToString(originalForm.TaskSequence[8]);
            g9.Text = Convert.ToString(originalForm.TaskSequence[9]);

            t1.Text = Convert.ToString(originalForm.CurCntSequence[0]);
            t2.Text = Convert.ToString(originalForm.CurCntSequence[1]);
            t3.Text = Convert.ToString(originalForm.CurCntSequence[2]);
            t4.Text = Convert.ToString(originalForm.CurCntSequence[3]);
            t5.Text = Convert.ToString(originalForm.CurCntSequence[4]);
            t6.Text = Convert.ToString(originalForm.CurCntSequence[5]);
            t7.Text = Convert.ToString(originalForm.CurCntSequence[6]);
            t8.Text = Convert.ToString(originalForm.CurCntSequence[7]);
            t9.Text = Convert.ToString(originalForm.CurCntSequence[8]);
            t10.Text = Convert.ToString(originalForm.CurCntSequence[9]);

            algoUsedTB.Text = originalForm.schedulingUsed;
        }

    }
}
