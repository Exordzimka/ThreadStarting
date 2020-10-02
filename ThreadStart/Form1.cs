using System;
using System.Threading;
using System.Windows.Forms;

namespace ThreadStart
{
    public partial class Form1 : Form
    {
        private int[] _array;
        public Form1()
        {
            InitializeComponent();
        }

        private delegate void FillingDelegate(ListBox listBox, int[] array);
        private void button1_Click(object sender, EventArgs e)
        {
            FillingArray(int.Parse(textBox1.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BubbleSort();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShellSort();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InsertSort();
        }

        private void FillingArray(int size)
        {
            _array = new int[size];
            Random random = new Random();
            listBox1.Items.Clear();
            for (int i = 0; i < size; i++)
            {
                _array[i] = random.Next(10, 100);
                listBox1.Items.Add(_array[i]);
            }
        }

        private void BubbleSort()
        {
            var tempArray = (int[])_array.Clone();
            for (var i = 0; i < tempArray.Length; i++)
            {
                var stop = true;
                for(var j=0;j< tempArray.Length-1;j++)
                {
                    if (tempArray[j + 1] >= tempArray[j]) continue;
                    var temp = tempArray[j];
                    tempArray[j] = tempArray[j + 1];
                    tempArray[j + 1] = temp;
                    stop = false;
                }
                if (stop)
                    break;
            }
            
            FillingList(listBox2, tempArray);
        }

        private void InsertSort()
        {
            int[] tempArray = (int[])_array.Clone();

            for (int i = 0; i < tempArray.Length - 1; i++)
            {
                for (int j = i + 1; j > 0; j--)
                {
                    if (tempArray[j - 1] > tempArray[j])
                    {
                        int temp = tempArray[j - 1];
                        tempArray[j - 1] = tempArray[j];
                        tempArray[j] = temp;
                    }
                }
            }
            
            FillingList(listBox4, tempArray);
        }

        private void ShellSort()
        {
            var tempArray = (int[])_array.Clone();
            int step;
            for (step = tempArray.Length / 2; step > 0; step /= 2)
            {
                int i;
                for (i = step; i < tempArray.Length; i++)
                {
                    var tmp = tempArray[i];
                    int j;
                    for (j = i; j >= step; j -= step)
                    {
                        if (tmp < tempArray[j - step])
                            tempArray[j] = tempArray[j - step];
                        else
                            break;
                    }
                    tempArray[j] = tmp;
                }
            }
           
            FillingList(listBox3, tempArray);
        }

        private void FillingList(ListBox listBox, int[] array)
        {
            Action action = () =>
            {
                Filling(listBox, array);
            };
            
            if(InvokeRequired)
            {
                Invoke(action);
                //BeginInvoke(Filling(listBox, array));
            }
            else
            {
                action();
            }
        }

        private static void Filling(ListBox listBox, int[] array)
        {
            listBox.Items.Clear();
            foreach (var item in array)
            {
                listBox.Items.Add(item);
            }
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            var thread1 = new Thread(BubbleSort);
            var thread2 = new Thread(InsertSort);
            var thread3 = new Thread(ShellSort);
            thread1.Start();
            thread2.Start();
            thread3.Start();
        }
        
    }
}
