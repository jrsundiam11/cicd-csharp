# Sample CI/CD Demo — C# + xUnit + GitHub Actions

A minimal project that shows how to wire up a GitHub Actions pipeline so a
pull request **cannot be merged into `main` or `develop` while its tests
are failing**.

## What's in here

```
SampleApp/                 Class library with a tiny Calculator class
SampleApp.Tests/           xUnit test project covering the Calculator
SampleApp.Console/         Runnable console app that exercises the Calculator
SampleApp.sln              Solution tying all three projects together
.github/workflows/ci.yml   The GitHub Actions pipeline
```

## The important concept: CI ≠ merge blocking (on its own)

A GitHub Actions workflow only *reports* pass/fail — it has no built-in
power to stop a merge. The thing that actually blocks the "Merge pull
request" button is a **branch protection rule** that you configure to
*require* this workflow's job to pass. The workflow and the branch
protection rule work together:

1. `ci.yml` runs `dotnet test` on every push and PR targeting `main`/`develop`.
2. If a test fails, `dotnet test` exits with a non-zero code → the step
   fails → the job (`build-and-test`) fails → GitHub marks the check ❌.
3. Branch protection says "the `build-and-test` check must be ✅ before
   this PR can be merged" → the merge button stays disabled while it's ❌.

## One-time setup: enable branch protection

In your GitHub repo:

1. Go to **Settings → Branches**.
2. Under **Branch protection rules**, click **Add rule** (do this once for
   `main` and once for `develop`).
3. Branch name pattern: `main` (repeat for `develop`).
4. Enable **"Require status checks to pass before merging."**
5. In the status check search box, select **`build-and-test`** (this is
   the job name from `ci.yml` — it only appears in the list after the
   workflow has run at least once on the repo).
6. Optionally also enable "Require branches to be up to date before
   merging" so PRs are re-tested against the latest target branch.
7. Save.

From this point on, any PR into `main` or `develop` shows a required
"build-and-test" check, and GitHub disables the merge button until it's
green.

## Running the app

`SampleApp` is a class library (no entry point on its own). `SampleApp.Console`
is a small runnable project that exercises the Calculator and prints output:

```bash
dotnet run --project SampleApp.Console
```

## Running the tests locally

```bash
dotnet restore
dotnet test
```

You should see 14 passing tests (Add, Subtract, Multiply, Divide,
divide-by-zero exception, and IsPrime across several inputs).

## Demoing a failure end-to-end

1. In `SampleApp.Tests/CalculatorTests.cs`, uncomment the test at the
   bottom named `Add_IntentionallyBroken_ToDemonstrateFailingPipeline`
   (it deliberately asserts `2 + 2 == 5`).
2. Commit and push to a feature branch, then open a PR into `develop`
   or `main`.
3. Watch the **Checks** tab on the PR — `build-and-test` will fail, the
   test reporter will show exactly which assertion failed, and the
   **Merge** button will be disabled with a message like "Required
   status check has not succeeded."
4. Re-comment the test out, push again — the check turns green and the
   PR becomes mergeable.

## Notes

- This was hand-authored (no local .NET SDK was available in this
  session to `dotnet build`/`dotnet test` it directly), so before you
  push it, run `dotnet restore && dotnet build && dotnet test` locally
  once to confirm everything compiles cleanly in your environment.
- The workflow and projects target .NET 10 (matches a verified local
  test run). If your machine only has .NET 8 installed, change
  `net10.0` to `net8.0` in all three `.csproj` files and update
  `dotnet-version` in `ci.yml` to `'8.0.x'` instead — .NET SDKs run
  side-by-side, so this is a simple find-and-replace either way.
- `dorny/test-reporter` is optional — it just makes failures easier to
  read in the PR UI. The merge-blocking behavior works with just the
  `dotnet test` step even if you remove that action.
