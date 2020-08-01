$namesespace = "sailingactors"

$component = "sailingactors-actors"
$chart = "sailingactors"
$projectDir = ".\SailingActors.Actors"
$imgName = "sailingactors/actors"


$helmInstalls =  helm list -n $namesespace -o json | ConvertFrom-Json

$helm = $helmInstalls |  Where-Object {$_.name -eq $component }
$rev = ($helm.revision -as [int]) + 1

$tag = "0.1.$rev";

Write-Host "Deploying $component version $tag"

$REDIS_PASSWORD=(kubectl get secret --namespace redis redis -o jsonpath="{.data.redis-password}")
$REDIS_PASSWORD=[System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($REDIS_PASSWORD))

docker build -f "$projectDir\Dockerfile" . -t $imgName":"$tag --build-arg VERSION=$tag

Write-Host "Helm upgrade"


helm upgrade $component $projectDir\charts\$chart --install --namespace $namesespace --set image.tag=$tag --set secrets.redis.passphrase=$REDIS_PASSWORD --wait