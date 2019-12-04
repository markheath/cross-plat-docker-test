# Communicating Between Windows and Linux Containers on Docker Desktop

This simple demo project shows how you can run Windows and Linux containers simultaneously on Docker Desktop, and that they can access each other.

### Set up the Linux container

1. Set Docker into Linux container mode

2. Build the Linux container

```
docker image build -t crossplat:linux .
```

3. Run the Linux container, listening on port 32770

```
docker run -p 32770:80 -d -e ASPNETCORE_URLS="http://+80" -e FetchUrl="http://10.0.75.1:57000/?handler=data" crossplat:linux
```

### Set up the Windows container

1. Switch Docker to Windows containers mode (check with docker version)

2. Build the Linux container image

```
docker image build -t crossplat:win .
```

3. Run the Windows container, listening on port 57000

```
docker run -p 57000:80 -d -e ASPNETCORE_URLS="http://+80" -e FetchUrl="http://10.0.75.1:32770/?handler=data" crossplat:win
```

### Test it out

Visit the Windows Container at http://localhost:57000/ and the Linux container at
http://localhost:32770/

Notes:
There is a `--platform linux` switch that can be used with Docker in "experimental" mode, letting you run linux containers on Windows with "[LCOW](https://docs.microsoft.com/en-us/virtualization/windowscontainers/deploy-containers/linux-containers)". Might make this possible without switching between modes at all.