# Flappy Bird Clone

[Play the Game Here (WebGL Build)](https://storage.googleapis.com/flappy-bird-clone/index.html)

## 🛠 Game Engine
- **Unity**

## 🎮 About the Project
This project is a clone of the popular mobile game **Flappy Bird**, created using the Unity engine. It features two modes:
1. **Player Mode:** Play the game manually.
2. **AI Mode:** Watch as a **Genetic Neural Network (GNN)** learns to achieve a high score.

The GNN was implemented from scratch, with multiple agents working together to optimize the parameters needed to beat the game. The optimal network discovered has:
- **1 hidden layer** with **6 neurons** using the **ReLU activation function**.
- **1 output neuron** using the **Sigmoid function**.

## 📊 Parameters Used for the GNN
The network uses the following inputs:
- **x position** of the next pipe.
- **y position** of the top pipe.
- **y position** of the bottom pipe.
- **Bird's y position.**
- **Bird's y velocity.**

## 🏆 Rewards
The reward system was designed to encourage both horizontal and vertical fitness:
- **Horizontal Distance:** Rewards are given based on how far a bird progresses horizontally.
- **Vertical Positioning:** Birds closer to the pipe openings are rewarded more than those hitting the ground, even if they travel the same horizontal distance.

This combination ensures that birds prioritize reaching the pipe openings rather than relying on suboptimal strategies.

## ⚡ Challenges and Solutions
1. **Traversing the Hypothesis Space:**
   - **Challenge:** With fewer agents, the hypothesis space wasn’t explored efficiently, leading to suboptimal solutions.
   - **Solution:** Increased the population size to **90 birds** per generation. This balanced game performance with effective learning.

2. **Avoiding Suboptimal Strategies:**
   - **Challenge:** Some birds evolved to rely on suboptimal strategies, such as staying on the ground and flapping only when necessary, which performed well early on but failed with higher pipe variability.
   - **Solution:** Introduced **randomization** by spawning **10 random birds** with entirely new parameters in each generation. This ensured genetic diversity and helped avoid premature convergence to suboptimal solutions.

## 🚀 Key Takeaways
- Balancing computational resources and population size was crucial for efficient training.
- Introducing genetic diversity each generation helped overcome local optima and improved overall performance.

Feel free to check out the WebGL build and experiment with both modes!
