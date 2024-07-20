Math Puzzle Game
Welcome to the Math Puzzle Game! This project is a Unity-based educational game designed to challenge players with various math operations while progressing through different levels of increasing difficulty.

Table of Contents
Game Description
Features
Installation
How to Play
Levels and Difficulty
Project Structure
Scripts
Contributing
License

Game Description
Math Puzzle Game is an engaging way to practice math skills. Players solve math problems by dragging and dropping answers to the corresponding math operations. The game consists of three levels, each introducing new types of math operations to increase the difficulty.

Features
Multiple Levels: Three levels of increasing difficulty.
Level 1: Addition and Subtraction
Level 2: Addition, Subtraction, and Multiplication
Level 3: Addition, Subtraction, Multiplication, and Division
Timer: Each level is timed, adding an element of urgency to solve the puzzles.
Dynamic UI: The game dynamically generates math problems and scatters answer buttons within a specified area.
Progressive Difficulty: The difficulty increases with each level.
Visual Feedback: "Level Up" messages and win notifications enhance the player experience.

Installation
Clone the repository:

sh
Copy code
git clone https://github.com/yourusername/math-puzzle-game.git
Open in Unity:

Open Unity Hub.
Add the cloned project to your Unity projects.
Open the project in Unity.
How to Play
Start the Game:

Launch the game by clicking the play button in the Unity Editor.
Select Level:

The game starts with the level selection panel. Click the play button to start the first level.
Solve Math Puzzles:

Drag the correct answer buttons and drop them on the corresponding math operation buttons.
Progress Through Levels:

Solve all puzzles in a level to progress to the next level.
A "Level Up" message will be displayed when progressing to the next level.
Complete all levels to win the game.
Timer:

Each level has a timer. Complete the puzzles before time runs out.
If the timer runs out, the game is over.
Reset Game:

Click the reset button to start the game over from the first level.
Levels and Difficulty
Level 1: Focuses on addition and subtraction problems.
Level 2: Introduces multiplication problems in addition to addition and subtraction.
Level 3: Adds division problems to the mix, ensuring a challenging experience.
Project Structure
Assets: Contains all game assets, including scripts, prefabs, UI elements, and images.
Scripts: Contains the C# scripts for game functionality.
Prefabs: Reusable game objects such as buttons and UI elements.
Scripts
GameManager.cs
Handles the core game logic, including:

Initializing the game and UI elements.
Managing levels and difficulty.
Generating math operations based on the current level.
Handling user interactions and checking answers.
Managing the game timer and game over conditions.
Other Components
DropHandler.cs: Handles drag-and-drop functionality for math operations.
DragHandler.cs: Manages the dragging behavior of answer buttons.
Contributing
Contributions are welcome! To contribute:

Fork the repository.
Create a new branch (git checkout -b feature/your-feature-name).
Commit your changes (git commit -m 'Add some feature').
Push to the branch (git push origin feature/your-feature-name).
Open a Pull Request.
License
This project is licensed under the MIT License. See the LICENSE file for details.

Enjoy the game and happy learning!
