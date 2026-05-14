# How to Manage Gemini Skills and Git Sync

This guide explains how to install the custom `blazor-excel-crud` skill on a new machine and how to keep your skills synced with your GitHub repository.

---

## 1. Installing the Skill on a New Machine

Once you have cloned this repository, follow these steps to activate the custom scaffolding logic.

### Step A: Install the Skill
Open your terminal in the project root (where the `.skill` file is located) and run:

```powershell
# Install at the user level (available for all your projects)
gemini skills install blazor-excel-crud.skill --scope user
```

### Step B: Reload Gemini CLI
For the changes to take effect, you must tell the CLI to refresh its skill library:

```powershell
/skills reload
```

### Step C: Verify
Confirm the skill is active by checking the list:

```powershell
/skills list
```

---

## 2. How to Update and Push Skills to Remote

If you make changes to the skill (editing `SKILL.md` or adding new references) and want to update the shared package in GitHub:

### Step A: Package the Skill
From the parent directory of your skill folder, run the packager:

```powershell
# Syntax: node <path-to-packager> <skill-folder-path> <output-path>
# Example (assuming you have the skill-creator installed):
node C:\Path\To\skill-creator\scripts\package_skill.cjs blazor-excel-crud .
```

### Step B: Commit and Push to GitHub
Once the new `.skill` file is generated, sync it with your remote repository:

```powershell
# Stage the updated skill package
git add blazor-excel-crud.skill

# Commit the changes
git commit -m "Update blazor-excel-crud skill with new patterns"

# Push to remote
git push origin main
```

---

## 3. Using the Skill
Once installed, you can trigger this specialized workflow in any project by simply saying:
> "Use the **blazor-excel-crud** skill to scaffold a management interface for my Excel file."
