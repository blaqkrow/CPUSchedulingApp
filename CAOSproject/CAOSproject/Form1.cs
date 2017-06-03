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
    public partial class Form1 : Form
    {
        public struct proc
        {
           public int Priority; // process priority
           public int arrivalTime; // process arrival time 
           public int burstTime; //process burst time 
           public bool Done; // Has the process finished executing?
           public int turnaroundTime; // turnaround time process
           public int waitingTime; // waiting time for process
           public int completionTime; // completion time for process
          // public int CurCntSequence;
          // public int TaskSequence;
           public int startingTime;
           public int idleStart;
           
        }

       public string[] TaskSequence = new string[50]; // saves the current job in the CPU
       public string[] CurCntSequence = new string[50]; // completion time for process

       public proc[] process = new proc[5]; // new instance of proc for 5 different processes

        int i, j, n, CurCnt;

        int CurrentTaskNum;
        int CurrentTaskPriority;
        int CurrentTaskBurstTime;
        int CurrentTaskArrivalTime;
        int PreviousHighestPriorityNumber;
        int smallestBurstTime;
        bool first;
        bool AllDone;
        bool Found;

        public float AvgTT, AvgWT, TotalTT, TotalWT;
        public string schedulingUsed;
        
        int TaskSequenceIndex;

        public Form1()
        {
            InitializeComponent();
        }

        private void priorityBtn_Click(object sender, EventArgs e)
        {
            //Initialise variables and structs
            AllDone = false;
            CurrentTaskNum = 0;
            CurrentTaskPriority = 0;
            CurrentTaskBurstTime = 0;
            CurrentTaskArrivalTime = 0;
            PreviousHighestPriorityNumber = 0;
            AvgTT = 0;
            AvgWT = 0;
            TotalTT = 0;
            TotalWT = 0;
            TaskSequenceIndex = 0;
            CurCnt = 0;
            n = 5;

            //Current count sequence
            for (i = 0; i < 50; i++ )
                CurCntSequence[i] = " ";

            //Proccess Sequence (Ganht chart)

            for (i = 0; i < 50; i++)
                TaskSequence[i] = "idle";

            //Arrival Time
            process[0].arrivalTime = Convert.ToInt32(ATime0TB.Text);
            process[1].arrivalTime = Convert.ToInt32(ATime1TB.Text);
            process[2].arrivalTime = Convert.ToInt32(ATime2TB.Text);
            process[3].arrivalTime = Convert.ToInt32(ATime3TB.Text);
            process[4].arrivalTime = Convert.ToInt32(ATime4TB.Text);

            //burst times
            process[0].burstTime = Convert.ToInt32(BTime0TB.Text);
            process[1].burstTime = Convert.ToInt32(BTime1TB.Text);
            process[2].burstTime = Convert.ToInt32(BTime2TB.Text);
            process[3].burstTime = Convert.ToInt32(BTime3TB.Text);
            process[4].burstTime = Convert.ToInt32(BTime4TB.Text);

            //priority
            process[0].Priority = Convert.ToInt32(priority0TB.Text);
            process[1].Priority = Convert.ToInt32(priority1TB.Text);
            process[2].Priority = Convert.ToInt32(priority2TB.Text);
            process[3].Priority = Convert.ToInt32(priority3TB.Text);
            process[4].Priority = Convert.ToInt32(priority4TB.Text);

            //processes are NOT done
            process[0].Done = false;
            process[1].Done = false;
            process[2].Done = false;
            process[3].Done = false;
            process[4].Done = false;

            //Scheduling Start
            do
            {
                //Find the highest priority number that satisfy the search condition
                first = true;
                for (j = 0; j < n; j++)
                {
                    if ((process[j].arrivalTime <= CurCnt) && !(process[j].Done)) //<- Search condition: Arrival time must be LOWER than current count and process must be NOT DONE!!!
                    {
                        // To find the highest priority process that has just arrived
                        if (first) // in you're the first process to enter
                        {
                            first = false;
                            PreviousHighestPriorityNumber = process[j].Priority; // first task that satisfies condition will always be assigned "highest priority"
                        }

                        else
                        {
                            if (process[j].Priority < PreviousHighestPriorityNumber)
                                PreviousHighestPriorityNumber = process[j].Priority;
                        }
                    }
                }

                first = true;
                Found = false;

                for (j = 0; j < n; j++)
                {
                    if ((process[j].arrivalTime <= CurCnt) && !(process[j].Done) && (process[j].Priority == PreviousHighestPriorityNumber)) // process must have the highest priority
                    {
                        Found = true;

                        if (first) //<<< THIS ONLY RUNS ONCE!!!
                        { //Sets all first attributes in array to a variable each
                            first = false;
                            CurrentTaskNum = j;
                            CurrentTaskPriority = process[j].Priority;
                            CurrentTaskArrivalTime = process[j].arrivalTime;
                            CurrentTaskBurstTime = process[j].burstTime;
                        }

                        //else // if there are other processes that meet the search condition
                        //{
                        //    if (process[j].arrivalTime < CurrentTaskArrivalTime) // if the arrival time of current process is smaller than the smallest found so far
                        //    {
                        //        CurrentTaskNum = j;
                        //        CurrentTaskPriority = process[j].Priority;
                        //        CurrentTaskArrivalTime = process[j].arrivalTime;
                        //        CurrentTaskBurstTime = process[j].burstTime;
                        //    }
                        //}
                    }

                }

                if (Found) // if task is found, Compute current count, process completion time, turnaround time, and waiting time. Set complete task to Done = true.
                {
                    process[CurrentTaskNum].startingTime = CurCnt;
                    CurCnt = CurCnt + CurrentTaskBurstTime;
                    process[CurrentTaskNum].completionTime = CurCnt;
                    process[CurrentTaskNum].turnaroundTime = process[CurrentTaskNum].completionTime - process[CurrentTaskNum].arrivalTime;
                    process[CurrentTaskNum].waitingTime = process[CurrentTaskNum].turnaroundTime - process[CurrentTaskNum].burstTime;
                    process[CurrentTaskNum].Done = true;

                    TotalTT = TotalTT + process[CurrentTaskNum].turnaroundTime;
                    TotalWT = TotalWT + process[CurrentTaskNum].waitingTime;

                    TaskSequence[TaskSequenceIndex] = "P" + Convert.ToString(CurrentTaskNum); // Current process
                    CurCntSequence[TaskSequenceIndex] = Convert.ToString(CurCnt); // Proccess completion time
                    TaskSequenceIndex++;
                }

                else // no task is found, increase count
                {
                    TaskSequence[TaskSequenceIndex] = "Idle"; // Set current task to idle
                    CurCnt++;
                    CurCntSequence[TaskSequenceIndex] = Convert.ToString(CurCnt); // set the end time of idling
                    TaskSequenceIndex++;
                }

                 AllDone = process[0].Done;

                 for (i = 1; i < n; i++)
                 {
                     AllDone = AllDone && process[i].Done; // are all done?
                 }

            } while (!AllDone); // loop if all are not done


            AvgTT = TotalTT / n;
            AvgWT = TotalWT / n;

            schedulingUsed = "Priority";

            Form2 f2 = new Form2(this);

            f2.Show();

        }

        private void sjfBtn_Click(object sender, EventArgs e)
        {
            //Initialise variables and structs
            AllDone = false;
            CurrentTaskNum = 0;
            CurrentTaskBurstTime = 0;
            CurrentTaskArrivalTime = 0;
            smallestBurstTime = 0;
            AvgTT = 0;
            AvgWT = 0;
            TotalTT = 0;
            TotalWT = 0;
            TaskSequenceIndex = 0;
            CurCnt = 0;
            n = 5;

            //Current count sequence
            for (i = 0; i < 50; i++)
                CurCntSequence[i] = " ";

            //Proccess Sequence (Ganht chart)

            for (i = 0; i < 50; i++)
                TaskSequence[i] = "idle";

            //Arrival Time
            process[0].arrivalTime = Convert.ToInt32(ATime0TB.Text);
            process[1].arrivalTime = Convert.ToInt32(ATime1TB.Text);
            process[2].arrivalTime = Convert.ToInt32(ATime2TB.Text);
            process[3].arrivalTime = Convert.ToInt32(ATime3TB.Text);
            process[4].arrivalTime = Convert.ToInt32(ATime4TB.Text);

            //burst times
            process[0].burstTime = Convert.ToInt32(BTime0TB.Text);
            process[1].burstTime = Convert.ToInt32(BTime1TB.Text);
            process[2].burstTime = Convert.ToInt32(BTime2TB.Text);
            process[3].burstTime = Convert.ToInt32(BTime3TB.Text);
            process[4].burstTime = Convert.ToInt32(BTime4TB.Text);

            //processes are NOT done
            process[0].Done = false;
            process[1].Done = false;
            process[2].Done = false;
            process[3].Done = false;
            process[4].Done = false;

            //Scheduling Start
            do
            {
                //Find the highest priority number that satisfy the search condition
                first = true;
                for (j = 0; j < n; j++)
                {
                    if ((process[j].arrivalTime <= CurCnt) && !(process[j].Done)) //<- Search condition: PROCESSES MUST HAVE ARRIVED (Arrival time must be LOWER than current count) and process must be NOT DONE!!!
                    {
                        // To find the process with the smallest burst time that has just arrived
                        if (first) // if you're the first process to enter
                        {
                            first = false;
                            smallestBurstTime = process[j].burstTime; // first task that satisfies condition will always be assigned "Smallest Burst Time"
                        }

                        else
                        {
                            if (process[j].burstTime < smallestBurstTime) // if your current burst time is smaller than your previous burst time
                                smallestBurstTime = process[j].burstTime;
                        }
                    }
                }

                first = true;
                Found = false;

                for (j = 0; j < n; j++)
                {
                    if ((process[j].arrivalTime <= CurCnt) && !(process[j].Done) && (process[j].burstTime == smallestBurstTime)) // if there are 2 or more with the same priority
                    {
                        Found = true;

                        if (first) //<<< THIS ONLY RUNS ONCE!!!
                        { //Sets all first attributes in array to a variable each
                            first = false;
                            CurrentTaskNum = j;
                            CurrentTaskArrivalTime = process[j].arrivalTime;
                            CurrentTaskBurstTime = process[j].burstTime;
                        }

                        else // if there are other processes that meet the search condition
                        {
                            if (process[j].arrivalTime < CurrentTaskArrivalTime) // if the arrival time of current process is smaller than the smallest found so far
                            {
                                CurrentTaskNum = j;
                                CurrentTaskArrivalTime = process[j].arrivalTime;
                                CurrentTaskBurstTime = process[j].burstTime;
                            }

                            else if (process[j].arrivalTime == CurrentTaskArrivalTime)
                            {
                                if (j < CurrentTaskNum)
                                {
                                    CurrentTaskNum = j;
                                    CurrentTaskArrivalTime = process[j].arrivalTime;
                                    CurrentTaskBurstTime = process[j].burstTime;
                                }
                            }
                        }
                    }

                }

                if (Found) // if task is found, Compute current count, process completion time, turnaround time, and waiting time. Set complete task to Done = true.
                {
                    process[CurrentTaskNum].startingTime = CurCnt;
                    CurCnt = CurCnt + CurrentTaskBurstTime;
                    process[CurrentTaskNum].completionTime = CurCnt;
                    process[CurrentTaskNum].turnaroundTime = process[CurrentTaskNum].completionTime - process[CurrentTaskNum].arrivalTime;
                    process[CurrentTaskNum].waitingTime = process[CurrentTaskNum].turnaroundTime - process[CurrentTaskNum].burstTime;
                    process[CurrentTaskNum].Done = true;

                    TotalTT = TotalTT + process[CurrentTaskNum].turnaroundTime;
                    TotalWT = TotalWT + process[CurrentTaskNum].waitingTime;

                    TaskSequence[TaskSequenceIndex] = "P" + Convert.ToString(CurrentTaskNum); // Current process
                    CurCntSequence[TaskSequenceIndex] = Convert.ToString(CurCnt); // Proccess completion time
                    TaskSequenceIndex++;
                }

                else // no task is found, increase count
                {
                    TaskSequence[TaskSequenceIndex] = "Idle"; // Set current task to idle
                    CurCnt++;
                    CurCntSequence[TaskSequenceIndex] = Convert.ToString(CurCnt); // set the end time of idling
                    TaskSequenceIndex++;
                }

                AllDone = process[0].Done;

                for (i = 1; i < n; i++)
                {
                    AllDone = AllDone && process[i].Done; // are all done?
                }

            } while (!AllDone); // loop if all are not done


            AvgTT = TotalTT / n;
            AvgWT = TotalWT / n;

            schedulingUsed = "Shortest Job First";

            Form2 f2 = new Form2(this);

            f2.Show();

        }

        private void firstFitBtn_Click(object sender, EventArgs e)
        {

        }

        private void clearBtn_Click(object sender, EventArgs e) // clear all textboxes
        {
            ATime0TB.Text = "";
            ATime1TB.Text = "";
            ATime2TB.Text = "";
            ATime3TB.Text = "";
            ATime4TB.Text = "";

            BTime0TB.Text = "";
            BTime1TB.Text = "";
            BTime2TB.Text = "";
            BTime3TB.Text = "";
            BTime4TB.Text = "";

            priority0TB.Text = "";
            priority1TB.Text = "";
            priority2TB.Text = "";
            priority3TB.Text = "";
            priority4TB.Text = "";

            memReq0TB.Text = "";
            memReq1TB.Text = "";
            memReq2TB.Text = "";
            memReq3TB.Text = "";
            memReq4TB.Text = "";
        }
    }
}
