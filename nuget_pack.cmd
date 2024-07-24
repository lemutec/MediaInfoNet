@REM rd /s /q C:\Users\{UserName}\.nuget\packages\MediaInfoDLL
@REM dotnet nuget locals all --clear
cd pack
dotnet restore
dotnet pack MediaInfoNet.csproj -c Release -o ../
@pause
