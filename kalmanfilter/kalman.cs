using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace kalmanfilter
{
    class kalman
    {
        KalmanFilter KF;
        Mat TransitionM;
        Mat MeasurementM;
        Mat Measurement;
        Mat Predited;

        float[] A = new float[] {
            1, 0, 1, 0,
            0, 1, 0, 1,
            0, 0, 1, 0,
            0, 0, 0, 1};

        float[] H = new float[] {
            1, 0, 0, 0,
            0, 1, 0, 0};

        float[] H_no_measurement = new float[] {
            0, 0, 0, 0,
            0, 0, 0, 0};

        public kalman()
        {
            init();
        }

        public void init()
        {
            KF = new KalmanFilter(4, 2, 0);

            TransitionM = new Mat(new Size(4, 4), MatType.CV_32FC1);
            TransitionM.SetArray(0, 0, A);

            MeasurementM = new Mat(new Size(4, 2), MatType.CV_32FC1);
            MeasurementM.SetArray(0, 0, H);

            Measurement = new Mat(new Size(1, 2), MatType.CV_32FC1);
            Predited = new Mat(new Size(2, 1), MatType.CV_32FC1);

            TransitionM.CopyTo(KF.TransitionMatrix);
            MeasurementM.CopyTo(KF.MeasurementMatrix);

            //KF.MeasurementMatrix.SetIdentity(1);
            KF.ProcessNoiseCov.SetIdentity(1e-5);
            KF.MeasurementNoiseCov.SetIdentity(1e-1);
            KF.ErrorCovPost.SetIdentity(1);
        }

        public Mat update(float x, float y)
        {
            Measurement.Set(0, x);
            Measurement.Set(1, y);
            float g = Measurement.At<float>(0);
            float e = Measurement.Get<float>(0, 0);
            float f = Measurement.Get<float>(1, 0);

            //Predited = KF.Predict();

            Predited = KF.Correct(Measurement);
            return Predited;
        }

        public Mat update()
        {
            Predited = KF.Predict();
            

            return Predited;
        }
    }
}
