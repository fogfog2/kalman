using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenCvSharp;

namespace kalmanfilter
{
    class kalman
    {
        public kalman()
        {
            KalmanFilter KF;
            Mat TransitionM;

            float[] A = new float[] { 1, 1, 0, 1 };
            TransitionM = new Mat(new Size(2,2),MatType.CV_32FC1);
            TransitionM.SetArray(0, 0, A);

            KF = new KalmanFilter(4,2,0);

            TransitionM.CopyTo( KF.TransitionMatrix);
            KF.MeasurementMatrix.SetIdentity(1);
            KF.ProcessNoiseCov.SetIdentity(1e-5);
            KF.MeasurementNoiseCov.SetIdentity(1e-1);

        }
    }
}
