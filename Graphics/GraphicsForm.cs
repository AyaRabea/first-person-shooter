using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.Diagnostics;
using System;

namespace Graphics
{
   

    public partial class GraphicsForm : Form
    {
       
        Renderer renderer = new Renderer();
        Thread MainLoopThread;

        float deltaTime;
        public GraphicsForm()
        {
            InitializeComponent();
            simpleOpenGlControl1.InitializeContexts();

            MoveCursor();
            

            initialize();
            deltaTime = 0.005f;
            //MainLoopThread = new Thread(MainLoop);
            //MainLoopThread.Start();

        }
        void initialize()
        {
            renderer.Initialize();   
        }
        void MainLoop()
        {
            while (true)
            {
                renderer.Draw();
                renderer.Update(deltaTime);
                simpleOpenGlControl1.Refresh();

            }
        }
        private void GraphicsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            renderer.CleanUp();
            //MainLoopThread.Abort();
        }

        private void simpleOpenGlControl1_Paint(object sender, PaintEventArgs e)
        {
            renderer.Draw();
            renderer.Update(deltaTime);
        }
        bool moving;
        private void simpleOpenGlControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
            moving = false;
           // float speed = 7f;
          
            if (e.KeyChar == 'a')
            {
                renderer.playermove(move_direction.LEFT_X, renderer.cam.GetLookDirection());
                {
                    moving = true;
                }
                //    renderer.cam.Strafe(-speed);
                 //   moving = true;
               

            }
            if (e.KeyChar == 'd')
            {
                renderer.playermove(move_direction.RIGHT_X, renderer.cam.GetLookDirection());
                {
                    moving = true;
                }
                
                //  renderer.cam.Strafe(speed);
                //    moving = true;
               
            }
            if (e.KeyChar == 's')
            {
                renderer.playermove(move_direction.BACK_Z, renderer.cam.GetLookDirection());
                {
                    moving = true;
                }
                
                 //   renderer.cam.Walk(-speed);
                 //   moving = true;
                
            }
            if (e.KeyChar == 'w')
            {
                renderer.playermove(move_direction.FRONT_Z,renderer.cam.GetLookDirection());
                {
                    moving = true;
                }
                
               // renderer.cam.Walk(speed);
               //  moving = true;
                
               
               
            }

            if (e.KeyChar == 'p')
            {
                //renderer.m2.StartAnimation();
            }

           
            

            //label7.Text = "X: " + renderer.cam.mAngleX;
            //label8.Text = "Y: " + renderer.cam.mAngleY;

            simpleOpenGlControl1.Refresh();
        }

        float prevX, prevY;
        private void simpleOpenGlControl1_MouseMove(object sender, MouseEventArgs e)
        {
            float speed = 0.05f;
            float delta = e.X - prevX;
            if (delta > 2)
                renderer.cam.Yaw(-speed);
            else if (delta < -2)
                renderer.cam.Yaw(speed);


            delta = e.Y - prevY;
            if (delta > 2)
                renderer.cam.Pitch(-speed);
            else if (delta < -2)
                renderer.cam.Pitch(speed);

            MoveCursor();
            simpleOpenGlControl1.Refresh();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            //float r = float.Parse(textBox1.Text);
            //float g = float.Parse(textBox2.Text);
            //float b = float.Parse(textBox3.Text);
            //float a = float.Parse(textBox4.Text);
            //float s = float.Parse(textBox5.Text);
            //renderer.SendLightData(r, g, b, a, s);
        }

        private void simpleOpenGlControl1_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void simpleOpenGlControl1_MouseClick(object sender, MouseEventArgs e)
        {
            renderer.draw = true;
            renderer.cam.mAngleY += 0.1f;
        }

        private void MoveCursor()
        {
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Point p = PointToScreen(simpleOpenGlControl1.Location);
            Cursor.Position = new Point(simpleOpenGlControl1.Size.Width/2+p.X, simpleOpenGlControl1.Size.Height/2+p.Y);
            Cursor.Clip = new Rectangle(this.Location, this.Size);
            prevX = simpleOpenGlControl1.Location.X+simpleOpenGlControl1.Size.Width/2;
            prevY = simpleOpenGlControl1.Location.Y + simpleOpenGlControl1.Size.Height / 2;
        }

        private void simpleOpenGlControl1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void GraphicsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
