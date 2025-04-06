<h1 align="center" id="title">DnDiscordBot</h1>
  
<h2 align="center">👾 Discord bot for DnD players used as helper 👾</h2>

<h2> Features</h2>

Here're some of the project's features:

*  🎲 Dice rolls
*  🔮 5e spell list
*  🎯 5e classes

<h2>Discord server setup</h2>

To set up your Discord server for the bot, follow these steps:

1. **Create Channels for Features**:  
    Organize your server by creating dedicated text channels for each feature the bot provides. Here are the recommended channel names:
    - `#dice` for dice rolls.
    - `#spells` for accessing the 5e spell list.
    - `#class` for exploring 5e classes.

2. **Set Permissions**:  
    Ensure that the bot has the necessary permissions to read and send messages in these channels. You can do this by:
    - Right-clicking on the channel.
    - Selecting "Edit Channel" > "Permissions".
    - Adding the bot's role and enabling "Read Messages" and "Send Messages".

3. **Invite the Bot to Your Server**:  
    Use the bot's invite link to add it to your server. Make sure to grant it the required permissions during the invitation process.

4. **Test the Channels**:  
    Once the bot is added, test each channel by typing what you are looking for.
    For example:  
    - In `#dice`, try a dice roll command like `d20`.
    - In `#spells`, search for a spell using `fireball` or `fireb` for Fireball spell description.
    - In `#class`, explore a class with `wizard abjuration` or `wiz abj` for Wizard Abjuration class/subclass.

This setup ensures a clean and organized server for your DnD sessions!

<h2>Running the Project</h2>

To run this .NET project, follow these steps:

1. **Clone the Repository**:  
    ```bash
    git clone https://github.com/your-username/DnDiscordBot.git
    cd DnDiscordBot
    ```

2. **Install Dependencies**:  
    Ensure you have the .NET SDK installed. You can download it from [Microsoft's .NET website](https://dotnet.microsoft.com/). Then, restore the dependencies:  
    ```bash
    dotnet restore
    ```

3. **Configure the Bot**:  
    Create or update the `.env` file in the root directory of the project and add the following:

    ```env
    DISCORD_TOKEN=your-discord-bot-token
    ```

4. **Build the Project**:  
    Build the project to ensure everything is set up correctly:  
    ```bash
    dotnet build
    ```

5. **Run the Bot**:  
    Start the bot using the following command:  
    ```bash
    dotnet run
    ```
