using UnityEngine;

namespace Control.ANN
{
    public class Neuron
    {
        public readonly int inputNum;
        public readonly double[] inputs;
        public readonly double[] weights;
        public double output;
        public double bias;
        public double gradient;

        public Neuron(int inputNum)
        {
            this.inputNum = inputNum;
            inputs = new double[inputNum];
            weights = new double[inputNum];
            bias = Random.Range(-1.0f, 1.0f);
            for (int i = 0; i < inputNum; i++)
                weights[i] = Random.Range(-1.0f, 1.0f);
        }
    }
}