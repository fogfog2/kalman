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
        double x = 0;
        double y = 0;
        double dx = 1;
        double dy = 0;
        double radius = 0;
        double t = 0;
        double dt = 0.1;
        Mat img;
        Mat predicted_mat;
        kalman km = new kalman();
        List<Point> listpt;
        List<Point> listpt2;

        public cvCircleMove()
        {
            init();
            draw();

        }

        void init()
        {
            img = new Mat(new Size(300, 300), MatType.CV_8UC3);
            predicted_mat = new Mat(new Size(2, 1), MatType.CV_32FC1);
            listpt = new List<Point>();
            listpt2 = new List<Point>();
            radius = 100.0;
            x = 100.0;
            y = 100.0;
        }

        void draw()
        {
            int i = 0;
            while (true)
            {
                if (x < 10)
                    dx = 1;
                if (x > 290)
                    dx = -1;

                x = x + dx;

                y = radius * Math.Sin(t) + 150.0;


                Point pt2 = new Point(x, y);

                if (i % 10 == 3 || i % 10 == 6)
                {

                    Cv2.Circle(img, new Point(x, y), 1, new Scalar(0, 255, 0));
                    predicted_mat = km.update((float)x, (float)y);

                    listpt2.Add(pt2);
                }
                else
                    predicted_mat = km.update();

                float a = predicted_mat.Get<float>(0, 0);
                float b = predicted_mat.Get<float>(1, 0);

                Point pt = new Point(a, b);

                listpt.Add(pt);


                if(listpt.Count > 10)
                {
                    listpt.RemoveAt(0);
                }
                if(listpt2.Count>10)
                    listpt2.RemoveAt(0);

                for (int k = 1; k < listpt.Count; k++)
                {

                    Cv2.Line(img, listpt[k], listpt[k-1],new Scalar(255,0,0),1);
                }
                for (int k = 1; k < listpt2.Count; k++)
                {
                    Cv2.Line(img, listpt2[k], listpt2[k - 1], new Scalar(0, 255, 0), 1);
                }

                Cv2.Circle(img, new Point(a, b), 1, new Scalar(255, 0, 0));
                //Cv2.Circle(img, new Point(x, y), 1, new Scalar(0, 255, 0));
                Cv2.ImShow("t", img);

                if (Cv2.WaitKey(1) == 27)
                    break;
                img.SetTo(0);
                t = t + dt;
                i++;
            }
        }
    }
}
