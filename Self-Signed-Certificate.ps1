$certificate = New-SelfSignedCertificate `
-Type Custom `
-Provider "Microsoft Strong Cryptographic Provider" `
-Subject "CN=restaurantcert" `
-DnsName localhost `
-KeyAlgorithm RSA `
-KeyLength 2048 `
-KeyExportPolicy ExportableEncrypted `
-NotBefore (Get-Date) `
-NotAfter (Get-Date).AddYears(6) `
-CertStoreLocation Cert:\LocalMachine\My `
-FriendlyName "Localhost Certificate Restaurant Server" `
-HashAlgorithm SHA256 `
-KeyUsage DigitalSignature, KeyEncipherment, DataEncipherment `
-TextExtension @("2.5.29.37={text}1.3.6.1.5.5.7.3.1")

$certificatePath = 'Cert:\LocalMachine\My\' + ($certificate.ThumbPrint)
$pwd = ConvertTo-SecureString -String Test123 -Force -AsPlainText
$filePath = "src/Server/Restaurant.Server.Api/restaurantcert.pfx"

Export-PfxCertificate -cert $certificatePath -FilePath $filePath  -Password $pwd

Import-PfxCertificate -FilePath $filePath Cert:\CurrentUser\Root -Password $pwd -Exportable
