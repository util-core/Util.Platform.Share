Write-Host "install npm..."
yarn

Write-Host "ng build util-platform..."
ng build util-platform

Write-Host "npm start..."
npm start