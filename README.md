# TaskCli Application

## Setting Up the Environment Variable

To use the `TaskCli` application from the command line without specifying the full path to the executable, you need to set an environment variable to include the path to the `TaskCli` executable.

### For Windows

1. **Locate Your Executable:**
   Ensure you know the directory where `TaskCli.exe` is located. For example, if it is in `C:\Developer\Project\Publish\`, that is the path you will use.

2. **Open Environment Variables:**
   - Press `Win + R` to open the Run dialog.
   - Type `sysdm.cpl` and press Enter to open the System Properties window.
   - Go to the `Advanced` tab and click on `Environment Variables`.

3. **Edit the Path Variable:**
   - In the `System variables` section, find the `Path` variable and select it.
   - Click `Edit`.
   - In the `Edit Environment Variable` dialog, click `New` and add the directory path where `TaskCli.exe` is located (e.g., `C:\Developer\Project\Publish\`).

4. **Apply Changes:**
   - Click `OK` to close each dialog box.
   - You might need to restart your command prompt or computer for the changes to take effect.

5. **Verify Setup:**
   - Open a new command prompt.
   - Type `TaskCli` and press Enter. If everything is set up correctly, you should see the TaskCli application prompt or output.

### For macOS and Linux

1. **Locate Your Executable:**
   Ensure you know the directory where `TaskCli` is located.

2. **Open Terminal:**
   - Open a Terminal window.

3. **Edit Your Shell Configuration:**
   - For `bash`, open `~/.bashrc` or `~/.bash_profile`.
   - For `zsh`, open `~/.zshrc`.

   You can use a text editor such as `nano` or `vim`:
   ```bash
   nano ~/.bashrc  # or ~/.zshrc
