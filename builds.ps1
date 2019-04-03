ECHO "Building for Windows Server. Won't work if the project is already opened in Unity !"
& "C:\Program Files\Unity\Hub\Editor\2019.1.0b9\Editor\Unity.exe" -projectPath 'S:\bouergh\Documents\Unity\TopdownBossRoyale\' -executeMethod BuildEditorScript.ServerBuild -batchmode -logfile Builds\Windows_Server\build.log -quit  | Out-Null
ECHO "Building for Windows Client. Won't work if the project is already opened in Unity !"
& "C:\Program Files\Unity\Hub\Editor\2019.1.0b9\Editor\Unity.exe" -projectPath "S:\bouergh\Documents\Unity\TopdownBossRoyale\" -executeMethod BuildEditorScript.ClientBuild -batchmode -logfile Builds\Windows_Client\build.log -quit  | Out-Null

#Pas utile sur cette machine !
#ECHO "Building for Linux Server. Won't work if the project is already opened in Unity !"
#& "C:\Program Files\Unity\Hub\Editor\2019.1.0b9\Editor\Unity.exe" -projectPath "S:\bouergh\Documents\Unity\TopdownBossRoyale\" -executeMethod BuildEditorScript.ServerLinuxBuild -batchmode -logfile Builds\Linux_Server\build.log -quit  | Out-Null

ECHO "Launching a headless Windows Server and two Windows Clients on the machine !"
#si on lance le serveur avant, penser à ne pas attendre la fin du processus qui s'ouvre dans le terminal pour ouvrir les clients !
& "C:\Gitlab-Runner\Builds\Windows_Client\TDBR_Client.exe" -screen-height 480 -screen-width 640  -screen-quality Low -screen-fullscreen 0
& "C:\Gitlab-Runner\Builds\Windows_Client\TDBR_Client.exe" -screen-height 480 -screen-width 640  -screen-quality Low -screen-fullscreen 0
& "C:\Gitlab-Runner\Builds\Windows_Server\TDBR_Server.exe"