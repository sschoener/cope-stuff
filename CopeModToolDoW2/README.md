0. Make sure references to cope-Framework are working.
  * note that it links some of the references are hardcoded to DLLs directly instead of C# projects
  * look into the csproj files of the failing projects to figure out what DLLs are needed, build those in the framework
  * or maybe just update these to actually reference the csproj properly
1. Build project.
2. ImageViewerPlugin, RBFEditorPlugin, ScarPlugin files go into plugins\filetypes subdir.
3. Get archive.exe from DoW2 directory and put it into the tools\ subdir.