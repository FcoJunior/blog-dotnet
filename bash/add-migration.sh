#!/bin/bash

dotnet ef migrations add "$1" \
  --project ./../Blog.Infra/Blog.Infra.csproj \
  --startup-project ./../Blog.Api/Blog.Api.csproj \
  --output-dir ./../Blog.Infra/Persistence/Migrations
