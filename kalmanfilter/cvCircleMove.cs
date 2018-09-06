using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using kalmanfilter;
using OpenCvSharp;

namespace kalmanfilter
{
    class cvCircleMove
    {
        double x=0;
        double y=0;
        double dx=0;
        double dy=0 ;
        double radius = 0;
        double t = 0;
        double dt = 0.02;
        Mat img;
        public cvCircleMove()
        {
            kalman km = new kalman(); 
           
            init();
            draw();
        }

        void init()
        {
            img = new Mat(new Size(300,300),MatType.CV_8UC3);
            radius = 100.0;
            x = 100.0;
            y = 100.0;
            
        }

        void draw()
        {
            while(true)
            { 
                x = radius * Math.Cos(t) + 150.0;
                y = radius * Math.Sin(t) + 150.0;

                Cv2.Circle(img, new Point(x, y), 1, new Scalar(255, 255, 255));
                Cv2.ImShow("t", img);

                if (Cv2.WaitKey(1) == 27)
                    break;
                img.SetTo(0);
                t = t + dt;
            }
        }
    }
}
