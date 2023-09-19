# 🌱 Customizable Garden Unity Package

Welcome to the **Customizable Garden** Unity package. This package empowers developers to allow users to create their unique virtual environments. By leveraging an intuitive menu system 🎮, users can select, spawn, and modify in-game items to design their personalized garden spaces 🌼.

## 🌟 Features

### 📋 SpawnMenu:
- **Menu Content**: A script that outlines the customization of the spawning menu. Developers should:
  - Define details of each row (title and items) 📝.
  - Specify each item's attributes: title, icon, and the prefab to spawn 🌐.
  
- **SaveItemsToJSON**: To ensure persistence of the customized garden 💾, developers can serialize object data into a JSON file. This file uses the player's ID for naming (default ID is `0`). Upon restarting the scene, the menu script auto-loads the player's data.
  
- **Screenshot 📸**: Enables users to take a snapshot of their virtual environment.
  - Clicking the button initiates a countdown timer ⏲.
  - Captures the user's current view (UI excluded).
  - Saves the screenshot locally within the user's designated folder 📂.

### 🎲 Items:
- **InventoryItemManipulator**: A script attached to spawned game objects, allowing users to:
  - Delete ❌
  - Rotate 🔄
  - Resize 🔍
  To add a new prefab, simply append the `SpawnObjectManipulator` prefab to it. This prefab includes the `InventoryItemManipulator` script and associated UI. Please ensure the correct hitboxes and physics settings are applied to the new object.

### 📦 Chest:
- **Chest Prefab**: Offers a storage solution for players.
  - Allows voice 🎙 or text message recording.
  - Saves messages as cassettes 📼 or envelopes 💌.
  - Data storage is based on user ID (default is `0`).

- **Gallery Prefab 🖼**: A visual representation of user memories.
  - By default, it displays as a table filled with polaroid photos, representing captured moments 🌄.
  - Photos are linked to individual user IDs.

## 🚀 Getting Started
1. Import the **Customizable Garden** package.
2. Familiarize yourself with the prefabs and scripts 📚.
3. Begin designing your unique garden environment experience for your users 🎨.

Remember, this package is flexible, allowing for extensive customization and personalization. Let your imagination run wild 🌈, and provide your users with an unforgettable virtual gardening experience! 🌳🌺
