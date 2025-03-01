# Recursive Media Browser
This is a simple media browser written in C# using WinForms. It allows you to select a directory and then browse through files recursively.
It uses [LibVLC](https://www.videolan.org/vlc/libvlc.html) which means any file format supported by VLC should be playable.

## Building an Executable
You can use `dotnet publish -c Release --sc -p:PublishReadyToRun=true -p:PublishSingleFile=true` to create a single executable file.
Because trimming is not yet supported in projects using WinForms, the resulting file will be around 130 MB.
You can check the current state of trimming support [here](https://github.com/dotnet/winforms/issues/4649).

If trimming is supported in the future, this command should create a much smaller executable:  
`dotnet publish -c Release --sc -p:PublishReadyToRun=true -p:PublishSingleFile=true -p:PublishTrimmed=true`
