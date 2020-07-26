# Sailing Actors

Demo project to show dapr in conjonction with istio

## Installation

  - [Install Prometheus and Grafana](#install-prometheus-and-grafana)

### Install Prometheus and Grafana
source: https://github.com/dapr/docs/blob/master/howto/setup-monitoring-tools/setup-prometheus-grafana.md

1.  Create namespace for monitoring tool
renamed namezspace here to reuse monitroring with istio

```bash
kubectl create namespace observability
```

2. Install Prometheus

```bash
helm repo add stable https://kubernetes-charts.storage.googleapis.com
helm repo update
helm install monitoring stable/prometheus -n observability 
```

3. Install Grafana

```bash
helm install grafana stable/grafana -n observability 
```


```bash
kubectl get secret --namespace observability  grafana -o jsonpath="{.data.admin-password}" | base64 --decode ; echo
```

```powershell
$pass = kubectl get secret --namespace monitoring grafana -o jsonpath="{.data.admin-password}"; [Text.Encoding]::Utf8.GetString([Convert]::FromBase64String($pass));
```

### Install Jaeger
TODO standalone jaeger install

### Install Istio
For windows: https://github.com/istio/istio/releases/
(make sure you attributed enough memory to docker for desktop or minikube)
```powershell
istioctl manifest apply -f .\install\istio.local.yaml

kubectl apply -f .\install\enablemTls.yaml
```

### Install Dapr
source: https://github.com/dapr/docs/blob/master/getting-started/environment-setup.md

Install Dapr with istio Injection enabled, like this istio is responsible for mTLS
```bash
helm repo add dapr https://daprio.azurecr.io/helm/v1/repo
helm repo update

kubectl create namespace dapr-system
kubectl label namespace dapr-system istio-injection=enabled

helm install dapr dapr/dapr --namespace dapr-system  --set mtls.enabled=false
```

### Install Redis
```powershell
helm repo add bitnami https://charts.bitnami.com/bitnami
helm repo update

kubectl create namespace redis
kubectl label namespace redis istio-injection=enabled
helm upgrade --install redis bitnami/redis --namespace redis

```

