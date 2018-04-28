
namespace ARTag
{
    using UnityEngine;

    public class CoordinateUtils
    {

        public static Vector3 TransformPosition(Vector3 obj, Vector3 pos, Vector3 rot, bool isInverse = false)
        {
            Vector3 ori = obj - pos;

            float x = ori.x;
            float y = ori.y;
            float z = ori.z;

            float a = Mathf.Deg2Rad * rot.x;
            float b = Mathf.Deg2Rad * rot.y;
            float g = Mathf.Deg2Rad * rot.z;

            if (isInverse)
            {
                a = -a;
                b = -b;
                g = -g;
            }

            float[,] rx = new float[,] {
            { 1, 0, 0 },
            { 0, Mathf.Cos(a), -Mathf.Sin(a) },
            { 0, Mathf.Sin(a), Mathf.Cos(a) }
        };
            float[,] ry = new float[,] {
            { Mathf.Cos(b), 0, Mathf.Sin(b) },
            { 0, 1, 0 },
            { -Mathf.Sin(b), 0, Mathf.Cos(b) }
        };
            float[,] rz = new float[,] {
            { Mathf.Cos(g), -Mathf.Sin(g), 0 },
            { Mathf.Sin(g), Mathf.Cos(g), 0 },
            { 0, 0, 1 }
        };

            if (!isInverse)
            {
                return MatrixMult(rx, MatrixMult(ry, MatrixMult(rz, ori)));
            }
            else
            {
                return MatrixMult(rz, MatrixMult(ry, MatrixMult(rx, ori)));
            }
        }

        public static Vector3 TransformRevRotation(Quaternion rot, Quaternion refRot)
        {
            return rot.eulerAngles - refRot.eulerAngles;
        }

        public static Vector3 MatrixMult(float[,] rotationMatrix, Vector3 position)
        {
            float[] positionMatrix = new float[3];
            positionMatrix[0] = position.x;
            positionMatrix[1] = position.y;
            positionMatrix[2] = position.z;

            float[] output = new float[] { 0, 0, 0 };
            for (int row = 0; row < 3; row++)
            {
                output[row] = 0;
                for (int col = 0; col < 3; col++)
                {
                    output[row] += rotationMatrix[row, col] * positionMatrix[col];
                }
            }

            return new Vector3(output[0], output[1], output[2]);
        }
    }

}

