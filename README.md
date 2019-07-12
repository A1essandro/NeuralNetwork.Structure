# NeuralNetwork.Structure

[![Build status](https://ci.appveyor.com/api/projects/status/80anq3x27nlgk0w2/branch/master?svg=true)](https://ci.appveyor.com/project/A1essandro/neuralnetwork-structure/branch/master)

## Specification

#### Network

Interface [implementation](https://github.com/A1essandro/NeuralNetworkConstructor/tree/master/NeuralNetworkConstructor/Network) of `INetwork` is a container comprising layers (`ILayer`) interconnected by synapses (`Synapse`).

#### Layers

Interface [implementations](https://github.com/A1essandro/NeuralNetworkConstructor/tree/master/NeuralNetworkConstructor/Network/Layer) of `ILayer` are formal groups of `INode`.

#### Nodes

`INode` - [neurons](https://github.com/A1essandro/NeuralNetworkConstructor/blob/master/NeuralNetworkConstructor/Network/Node/Neuron.cs), [input-neurons](https://github.com/A1essandro/NeuralNetworkConstructor/blob/master/NeuralNetworkConstructor/Network/Node/InputNode.cs), [bias](https://github.com/A1essandro/NeuralNetworkConstructor/blob/master/NeuralNetworkConstructor/Network/Node/Bias.cs) etc.

#### Synapses

`Synapse` - is a connection between two nodes (`INode`). [Synapse](https://github.com/A1essandro/NeuralNetworkConstructor/tree/master/NeuralNetworkConstructor/Network/Node/Synapse) gets output (call `Output()`) from neuron-transmitter (implementation `INode` interface) and convert the value via its weight. Result value gets neuron-reciever (it call `Output()` of `ISynapse`).

## Contribute

Contributions to the package are always welcome!

## License

The code base is licensed under the MIT license.