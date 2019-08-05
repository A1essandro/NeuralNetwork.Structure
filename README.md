# NeuralNetwork.Structure

[![Build status](https://ci.appveyor.com/api/projects/status/80anq3x27nlgk0w2/branch/master?svg=true)](https://ci.appveyor.com/project/A1essandro/neuralnetwork-structure/branch/master)
[![Nuget](https://img.shields.io/nuget/v/NeuralNetwork.Structure)](https://www.nuget.org/packages/NeuralNetwork.Structure)
[![Nuget](https://img.shields.io/nuget/dt/NeuralNetwork.Structure)](https://www.nuget.org/packages/NeuralNetwork.Structure)

## Specification

### Network

Network is a container comprising layers and synapses. 

There are two network implementations: `SimpleNetwork` and `Network`.
`SimpleNetwork` contais only input and output layers. `Network` may contain inner layers.

```cs

using NeuralNetwork.Structure.Networks;

//...

new Network(); //creation of network
```

### Layers

Layer is a container for group of nodes. Network contains at least two layers - input and output.
`Network` contains collection of inner layers (may be empty).
It is implementation of `ICollection`, but there are no restrictions on the connection of neurons through the layers.
E.g node (neuron) from layer with index 0 may have synapse to node in layer with index 2.

There are two layer implementations: `InputLayer` and `Layer`.

```cs

using NeuralNetwork.Structure.Layers;

//...

network.InputLayer = new InputLayer(new InputLayer()); //create and init empty input layer for network
network.OutputLayer = new Layer(new Layer()); //create and init empty output layer for network

network.AddInnerLayer(new Layer()); //create and add empty input layer for network (only for Network, not for SimpleNetwork)

```

### Neurons (nodes)

There are three main implementations of nodes: `InputNode`, `Neuron` and `Bias`.
`Neuron` aggregates `IActivationFunction` and `ISummator`. [List of activation functions](https://github.com/A1essandro/NeuralNetwork.Structure/tree/master/NeuralNetwork.Structure/ActivationFunctions).

```cs

using NeuralNetwork.Structure.ActivationFunctions;
using NeuralNetwork.Structure.Layers;
using NeuralNetwork.Structure.Networks;
using NeuralNetwork.Structure.Nodes;
using NeuralNetwork.Structure.Summators;

//...

var inputNode0 = new InputNode();
var inputNode1 = new InputNode();
var outputNode = new Neuron(); //activation function and summator by default
var innerNode0 = new Neuron(new Rectifier(), new Summator());
var innerNode1 = new Neuron(new Quadratic(), new Summator());

network.InputLayer = new InputLayer(new InputLayer(inputNode0, inputNode1)); //create and init input layer for network
network.OutputLayer = new Layer(new Layer(outputNode)); //create and init output layer for network

network.AddInnerLayer(new Layer(innerNode, innerNode1)); //create and add inner layer for network (only for Network, not for SimpleNetwork)

```

### Synapses

`Synapse` is a connection between two nodes, from `INode` to `ISlaveNode` (`InputNode` and `Bias` is not `ISlaveNode`).

```cs

using NeuralNetwork.Structure.Synapses;

//...

var synapse = new Synapse(input, output, synapseWeight); //synapseWeight is not required parameter
network.AddSynapse(synapse);

```

## Input/Output

### Input

Use the `Input` method to enter input data:

```cs

ICollection<double> inputData = ...;
await network.Input(inputData);

```

### Output

After calling `Input` method, you can use method `Output`:

```cs

IEnumerable<double> outputData = await network.Output();

```

Also you can subscribe to the event `OnResultCalculated`:

```cs

//n - is the network (ISimpleNetwork)
//v - is the calculated result (IEnumerable<double>)
network.OnResultCalculated += (n, v) =>
{
    result = v;
    return Task.CompletedTask;
};

```

When using both variants, the event will be processed first.

## Learning

This repository does not contains learnig. But you can see [this repository](https://github.com/A1essandro/NeuralNetwork.Learning).

## Contribute

Contributions to the package are always welcome!

## License

The code base is licensed under the MIT license.