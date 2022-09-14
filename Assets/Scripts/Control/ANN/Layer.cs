namespace Control.ANN
{
    public class Layer
    {
        public readonly int neuronNum;
        public readonly Neuron[] neurons;

        public Layer(int neuronNum, int inputNum)
        {
            this.neuronNum = neuronNum;
            neurons = new Neuron[neuronNum];
            for (int i = 0; i < neuronNum; i++)
                neurons[i] = new Neuron(inputNum);
        }
    }
}