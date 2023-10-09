Write-Host "install npm..."
yarn --ignore-optional

Write-Host "ng build util-platform..."
ng build util-platform

Write-Host "npm start..."
npm start