# Hands-On Lab: Launch and Configure an EC2 Instance with a Web Server

This lab guides you through launching a t2.micro EC2 instance with Amazon Linux 2 AMI, configuring security groups, connecting via SSH, installing Apache web server, and hosting a simple web page.

---

## Prerequisites

- AWS Free Tier account with IAM user access (preferably with EC2FullAccess permissions)
- AWS CLI installed and configured
- SSH client installed (OpenSSH for Mac/Linux/Windows)
- Internet access and web browser for AWS Management Console
- (Optional) Smartphone with MFA app if root account access is needed

---

## Task 1: Launch an EC2 Instance

### Step 1.1: Access AWS Console

1. Navigate to [AWS Management Console](https://aws.amazon.com/console/)
2. Log in with your IAM user credentials
3. Ensure you're in a Free Tier supported region (e.g., **us-east-1**)

### Step 1.2: Navigate to EC2

1. Search for "EC2" in the Console or select EC2 from the services menu
2. Click **Instances** > **Launch instances**

### Step 1.3: Configure Instance Settings

**Name and Tags:**
- Instance name: `My-Web-Server`

**Application and OS Images (AMI):**
- Select **Amazon Linux 2 AMI (HVM), SSD Volume Type** (Free Tier eligible)

**Instance Type:**
- Choose **t2.micro** (Free Tier eligible: 1 vCPU, 1 GiB memory)

**Key Pair (Login):**
1. Click **Create new key pair**
2. Configure the key pair:
   - Key pair name: `my-ec2-key`
   - Key pair type: **RSA**
   - Private key file format: **.pem**
3. Click **Create key pair** and download `my-ec2-key.pem`
4. Store the file securely (e.g., `~/Downloads/my-ec2-key.pem`)
5. Set correct permissions:
   ```bash
   chmod 400 ~/Downloads/my-ec2-key.pem
   ```

**Network Settings:**
- VPC: Use the **default VPC**
- Subnet: Select **No preference** (uses default subnet)
- Auto-assign public IP: **Enable** (required for SSH and HTTP access)

**Firewall (Security Groups):**
1. Select **Create security group**
2. Security group name: `Web-Server-SG`
3. Description: `Security group for web server access`
4. Add the following rules:
   - **Rule 1:**
     - Type: **SSH**
     - Protocol: TCP
     - Port: **22**
     - Source: **My IP** (or 0.0.0.0/0 for simplicity, but less secure)
   - **Rule 2:**
     - Type: **HTTP**
     - Protocol: TCP
     - Port: **80**
     - Source: **Anywhere** (0.0.0.0/0)

**Storage:**
- Keep default: 1x 8 GiB gp2 volume (Free Tier eligible)

**Advanced Details:**
- Leave defaults (no IAM role needed for this lab)

### Step 1.4: Launch Instance

1. Click **Launch instance**
2. Go to **EC2** > **Instances** and confirm `My-Web-Server` is in **"Running"** state
3. Note the instance's **Public IPv4 address** (e.g., 54.123.45.67)

### Troubleshooting

- **Instance Not Launching:** Ensure t2.micro and Amazon Linux 2 AMI are selected
- **No Public IP:** Verify "Auto-assign public IP" is enabled
- **Security Group Issues:** Ensure SSH (port 22) and HTTP (port 80) rules are configured correctly

---

## Task 2: Connect to the EC2 Instance

### Step 2.1: Connect via SSH

1. Open a terminal (Mac/Linux/Windows)
2. Connect using the downloaded key pair:
   ```bash
   ssh -i ~/Downloads/my-ec2-key.pem ec2-user@<public-ip>
   ```
   
   **Example:**
   ```bash
   ssh -i ~/Downloads/my-ec2-key.pem ec2-user@54.123.45.67
   ```

3. If prompted, type `yes` to accept the host key

**Expected Output:** Terminal prompt like `[ec2-user@ip-xxx-xxx-xxx-xxx ~]$`

### Step 2.2: Verify Connection

Run the following command:
```bash
whoami
```

**Expected Output:** `ec2-user`

### Step 2.3: Test System Information

```bash
uname -a
```

**Expected Output:** Information about the Amazon Linux 2 system (kernel version, etc.)

### Step 2.4: Alternative - EC2 Instance Connect (Browser-Based)

1. In AWS Console, go to **EC2** > **Instances** > Select `My-Web-Server`
2. Click **Connect** > **EC2 Instance Connect** tab
3. Ensure User name is `ec2-user`
4. Click **Connect**

**Expected Output:** Browser-based terminal opens with EC2 prompt

### Troubleshooting

- **SSH Connection Refused:** Verify security group allows port 22 from your IP and instance is running
- **Permission Denied:** Ensure key pair file has correct permissions (`chmod 400`) and using `ec2-user` as username
- **EC2 Instance Connect Fails:** Ensure instance is in public subnet with public IP and security group allows SSH

---

## Task 3: Install and Configure a Web Server

### Step 3.1: Connect to Instance

Connect via SSH (as shown in Task 2):
```bash
ssh -i ~/Downloads/my-ec2-key.pem ec2-user@<public-ip>
```

### Step 3.2: Install Apache Web Server

**Update the instance:**
```bash
sudo yum update -y
```

**Install Apache HTTP Server:**
```bash
sudo yum install httpd -y
```

**Start Apache:**
```bash
sudo systemctl start httpd
```

**Enable Apache to start on boot:**
```bash
sudo systemctl enable httpd
```

**Verify Apache is running:**
```bash
sudo systemctl status httpd
```

**Expected Output:** Shows `active (running)` status

### Step 3.3: Create a Simple HTML Page

**Navigate to Apache web directory:**
```bash
cd /var/www/html
```

**Create an index.html file:**
```bash
sudo nano index.html
```

**Add the following HTML content:**
```html
<!DOCTYPE html>
<html>
<head>
    <title>My AWS Web Server</title>
</head>
<body>
    <h1>Welcome to My AWS EC2 Web Server!</h1>
    <p>This is a simple web page hosted on an EC2 instance.</p>
</body>
</html>
```

**Save and exit:** Press `Ctrl+O`, `Enter`, then `Ctrl+X`

**Set correct permissions:**
```bash
sudo chmod 644 /var/www/html/index.html
sudo chown apache:apache /var/www/html/index.html
```

### Troubleshooting

- **Apache Not Running:** Restart Apache and check status:
  ```bash
  sudo systemctl restart httpd
  sudo systemctl status httpd
  ```
- **Permission Issues:** Verify `index.html` permissions and ownership (`apache:apache`)

---

## Task 4: Test and Clean Up

### Step 4.1: Access the Web Page

1. Open a web browser
2. Enter the instance's public IP address:
   ```
   http://<public-ip>
   ```
   
   **Example:** `http://54.123.45.67`

**Expected Output:** Web page displays "Welcome to My AWS EC2 Web Server!"

### Step 4.2: Verify Apache Logs (Optional)

Check Apache access logs:
```bash
sudo cat /var/log/httpd/access_log
```

**Expected Output:** Shows HTTP requests from your browser

### Step 4.3: Exit the Terminal

```bash
exit
```

### Step 4.4: Clean Up Resources

**Terminate EC2 Instance:**
1. Go to **EC2** > **Instances**
2. Select `My-Web-Server`
3. Click **Instance state** > **Terminate instance**

**Key Pair and Security Group:**
- **Keep** `my-ec2-key.pem` for future labs (no cost)
- **Keep** `Web-Server-SG` for future labs (no cost)

**Billing Check:**
- Verify no unexpected charges in AWS Billing Console
- Set up billing alerts if not already configured

### Troubleshooting

- **Web Page Not Accessible:** Ensure security group allows HTTP (port 80) from 0.0.0.0/0 and instance has public IP
- **Browser Errors:** Ensure using `http://` (not `https://`) with correct public IP

---

## Expected Outcomes

✅ t2.micro EC2 instance launched with Amazon Linux 2 AMI  
✅ Security group configured for SSH (port 22) and HTTP (port 80)  
✅ Successfully connected to instance via SSH  
✅ Apache web server installed, started, and hosting HTML page  
✅ Web page accessible via instance's public IP in browser  

---

## Important Tips

- **Store** the key pair file (`my-ec2-key.pem`) securely; it cannot be re-downloaded
- **Always terminate** unused EC2 instances to stay within Free Tier limits
- **Monitor usage** in AWS Billing Console and set billing alerts
- **Refer to documentation:**
  - [AWS EC2 Documentation](https://docs.aws.amazon.com/ec2/)
  - [Amazon Linux 2 Documentation](https://docs.aws.amazon.com/linux/)

---

## Quick Reference Commands

```bash
# Set key pair permissions
chmod 400 ~/Downloads/my-ec2-key.pem

# Connect via SSH
ssh -i ~/Downloads/my-ec2-key.pem ec2-user@<public-ip>

# Update and install Apache
sudo yum update -y
sudo yum install httpd -y

# Manage Apache service
sudo systemctl start httpd
sudo systemctl enable httpd
sudo systemctl status httpd

# Create web page
cd /var/www/html
sudo nano index.html

# Set permissions
sudo chmod 644 /var/www/html/index.html
sudo chown apache:apache /var/www/html/index.html

# View logs
sudo cat /var/log/httpd/access_log
```
