## Step 1: Setup

Clone the repo
Remove the connection to the old git
Make my own repo on github and connect

## Step 2: Deploy azure app service
1. **Find app services on azure and click on the create button**
2. Create a new resource group
3. Choose a name
4. **Runtime Stack** (e.g., .NET, Node.js, Python). Choose **Linux** or **Windows** as the OS.
5. A region where the app will be hosted
6. A pricing plan (F1 cheapest 0$)
7. Zone redundancy disabled makes the avalibility higher but costs money
8. No database
9. **Continuous deployment enabled**
10. Connect to Github account
11. Enable public access
12. **Enable Application Insights**
13. **Review and Create**

A .YML file will be generated that triggers on pushes to main branch or a manually. This will update your app then redoply it shortly after. Contains in it the necessery commands to build, publish and upload the app.
The app gets built and packaged and finally the compiled package is then uploaded to Azure under `wwwroot`, where files are served statically (depending on framework/runtime used)

## Step 3: Application insights 

Make sure to activate Application Insight when configuring app service in the begining otherwise you can go down to the setting and also turn it on that way:

    Settings -> Monitoring -> Application Insights: Turn it on and connect to a resource.
  
With **Application Insights** enabled, you can monitor your application's behavior and troubleshoot issues directly from the Azure portal.
There is a  **Search** tab, where you can browse through all telemetry data, such as requests, traces, exceptions, and dependencies. This gives you a chronological view of what’s happening in your app.

## Step 4: Configure Security

1. **Access Control (IAM)** : You can choose to gran certain users diffrent levels of access.

       App Service -> Access Control (IAM) -> Add -> Add Role Assignment -> Reader (A user than has read access) -> Find a user to asign this too ->  Review and Assign

2. **SSL / HTTPS**  
Azure assigns a default domain (`yourapp.azurewebsites.net`).  
This domain is automatically secured with a **free SSL certificate**, which enforces **HTTPS** for secure, encrypted communication.


## Auto scaling

Only avaliable to higher tiers. Requires Premium v2 or Premium v3

App service -> Settings -> Scale out -> Automatic

This is a feature used to automaticly scale your app based on demand.

## Storage account

1. Find and add a new storage account 
2. Connect to your resource group and name the storage account
3. Pick a region  
4. Choose Azure Blob storage as primary service 
5. Standard performance
6. LRS: Locally-redundant storage
7. In advanced settings choose cool for the acces tier as well be accesing data infrequently
8. read and review

*The steps not mentioned just choose the defualt settigns given as the suffice*

### Uploading and using files

To upload and use files for your storage you will need to configure a **container**.

1. Data storage -> Containers
2. New container (+) and then give it a name
3. Now that you have a container you can upload files and use your files.

Before that its best to either configure a SAS (Shared Access Signature) or allow anonymous access to container.

To allow anonymous access go to storage account -> Settings -> configuration -> Allow Blob anonymous access (Enable) -> Back to container -> Change access level -> Blob or container.

**SAS:**

Settings -> shared access tokens -> configure and generate

now you can use this token at the end of your requests and this allows you to access your blobs (read,write,delete etc..)

## Key vaults

1. Find Key Vaults
2. Create
3. Unique name
4. Region (same as resource)
5. Standard
6. Azure role-based access control
7. Public endpoint 
8. Review and create

Then you go too Access control (IAM) and give your account the Key Vault Administrator role

Secrets -> Generate/Import -> Add a name and a value.

You should then with a few nugget packages ("Azure.Extensions.AspNetCore.Configuration.Secrets" 
, "Azure.Identity", "Azure.Security.KeyVault.Secrets") be able to get your key vault key using the url azure provides you for the key vault. I had some issues with authorization so decided to mock this part for now.



## Setting Up Azure DevOps CI/CD with GitHub

1. Set Up Azure DevOps
- Sign in or create an account at [https://dev.azure.com](https://dev.azure.com).
- Create a new **Project**:
  - Enter a project name.
  - Optionally, add a description.
  - Choose project visibility Public.

---


2. Connect Your GitHub Repository
- Navigate to the **Repos** section in your Azure DevOps project.
- There are multiple ways to import your code, but the easiest:
  1. Clone your existing GitHub repository (Can be done manually with Azures UI):
     ```bash
     git clone <your-github-repo-url>
     git remote add azure <your-azure-repo-url>
     git push azure main
     ```
  2. Push the cloned repo to your Azure DevOps project if needed.

---

 3. Set Up the Pipeline
- Go to **Pipelines > Create Pipeline**.
- Choose **GitHub** as your source.
- Sign in and connect your GitHub account.
- Select the desired repository.
- Azure DevOps will auto-generate a `azure-pipelines.yml` file based on your project.
- Review the YAML, then click **Run**.

---

### ⚠️ Common Issue

*[error] No hosted parallelism has been purchased or granted. To request a free parallelism grant, please fill out the following form: https://aka.ms/azpipelines-parallelism-request*

this means your organization doesn't have parallelism enabled yet.
you can request free parallelism via [this form](https://aka.ms/azpipelines-parallelism-request).

#### Notes

- **Trigger Differences**:
  - In **GitHub Actions**, the trigger (`on:`) must be explicitly defined in the `.yml` file to enable automatic or manual runs.
  - In **Azure Pipelines**, you can trigger builds manually even if no `trigger:` is defined in the YAML — the UI allows manual runs by default.

---











