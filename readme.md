1. export PATH="$PATH:$HOME/.dotnet/tools/"
2.dotnet ef dbcontext scaffold -o ./Models "Pomelo.EntityFrameworkCore.MySql" "Server=172.17.0.3;Database=ACB-System;user=root;pwd=admin;"