using System.Collections.Generic;
using UnityEngine;

namespace App.Control
{
    public class Neuron
    {
        public int inputNum;
        public double[] inputs;
        public double[] weights;
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

    public class Layer
    {
        public int neuronNum;
        public Neuron[] neurons;

        public Layer(int neuronNum, int inputNum)
        {
            this.neuronNum = neuronNum;
            neurons = new Neuron[neuronNum];
            for (int i = 0; i < neuronNum; i++)
                neurons[i] = new Neuron(inputNum);
        }
    }

    public class ANN
	{
        public int inputNum;
        public int outputNum;
        public int hiddenNum;
        public int perHiddenNeuronNum;
        public double alpha;
        Layer[] layers;

        public ANN(int inputNum, int outputNum, int hiddenNum, int perHiddenNeuronNum, double alpha)
        {
            this.inputNum = inputNum;
            this.outputNum = outputNum;
            this.hiddenNum = hiddenNum;
            this.perHiddenNeuronNum = perHiddenNeuronNum;
            this.alpha = alpha;
            layers = new Layer[hiddenNum + 1];

            layers[0] = new Layer(perHiddenNeuronNum, inputNum);
            for (int i = 1; i < hiddenNum; i++)
                layers[i] = new Layer(perHiddenNeuronNum, perHiddenNeuronNum);
            layers[hiddenNum] = new Layer(outputNum, perHiddenNeuronNum);
        }

        public List<double> CalcOutput(List<double> inputs)
        {
            List<double> tempInputs = new List<double>();
            List<double> tempOutputs = new List<double>();

            tempInputs = new List<double>(inputs);
            for (int i = 0; i < layers.Length; i++)
            {
                if (i > 0)
                    tempInputs = new List<double>(tempOutputs);
                tempOutputs.Clear();
                for (int j = 0; j < layers[i].neuronNum; j++)
                {
                    double equ = 0;
                    for (int k = 0; k < layers[i].neurons[j].inputNum; k++)
                    { 
                        layers[i].neurons[j].inputs[k] = tempInputs[k];
                        equ += layers[i].neurons[j].weights[k] * tempInputs[k];
                    }
                    equ -= layers[i].neurons[j].bias;
                    layers[i].neurons[j].output = (i == hiddenNum) ? Sigmoid(equ) : ReLu(equ);
                    tempOutputs.Add(layers[i].neurons[j].output);
                }
            }
            return tempOutputs;
        }

        public void UpdateANN(List<double> realOutputs, List<double> idealOutputs)
        {
            double error;
            for (int i = hiddenNum; i >= 0; i--)
            {
                for (int j = 0; j < layers[i].neuronNum; j++)
                {
                    if (i == hiddenNum)
                    {
                        error = idealOutputs[j] - realOutputs[j];
                        layers[i].neurons[j].gradient = realOutputs[j] * (1 - realOutputs[j]) * error;
                    }
                    else
                    {
                        layers[i].neurons[j].gradient = layers[i].neurons[j].output * (1 - layers[i].neurons[j].output);
                        double gradSum = 0;
                        for (int k = 0; k < layers[i + 1].neuronNum; k++)
                            gradSum += layers[i + 1].neurons[k].gradient * layers[i + 1].neurons[k].weights[j];
                        layers[i].neurons[j].gradient *= gradSum;
                    }

                    for (int k = 0; k < layers[i].neurons[j].inputNum; k++)
                    {
                        if (i == hiddenNum)
                        {
                            error = idealOutputs[j] - realOutputs[j];
                            layers[i].neurons[j].weights[k] += alpha * layers[i].neurons[j].inputs[k] * error;
                        }
                        else
                        {
                            layers[i].neurons[j].weights[k] += alpha * layers[i].neurons[j].inputs[k] * layers[i].neurons[j].gradient;
                        }
                    }
                    layers[i].neurons[j].bias -= alpha * layers[i].neurons[j].gradient;
                }
            }
        }

        double ReLu(double x)
        { 
            if(x > 0)
                return x;
            return 0;
        }

        double Sigmoid(double x)
        {
            double v = (double)System.Math.Exp(x);
            return v / (1.0 + v);
        }

        double TanH(double x)
        {
            double v = (double)System.Math.Exp(-2 * x);
            return 2 / (1.0 + v) - 1;
        }

        double Step(double x)
        {
            if (x > 0)
                return 1;
            return 0;
        }
	}
}
