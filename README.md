# Serko Travel API - Coding excercise
### Steps to run application
1. Download source code
2. Open the solution in Visual Studio, perform package restore by right-click on solution and choose "Enable NuGet Package Restore"
3. Build/Compile application
4. Run application by following ways
   - Click F5/Ctrl+F5 in visual studio
5. Launch postman
   - Enter url http://localhost:YourVisualStudioPort/api/serko/
   - Select POST request
   - Select Body tab, select raw checkbox, select text from message format dropdownlist
   - Copy email content from coding exercise
   - Click Send
   
### My Assumption
   - API receive block text, it should be no performance concern to processing text. As result, I choose synchronous API over asynchronous API
   - Replace all the emails with empty string so system can load without XML syntax exception. Other than that, system is able to handle missing open/close xml tags
   - Data return to user in JSON format and keep orginal values including special characters such as carriage return, tab
   - No security consideration
   - Missing <total> or <cost_center> mean its value doesn't supply
   
### Happy Coding