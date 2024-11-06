import Versions from './components/Versions'
import electronLogo from './assets/electron.svg'

function App(): JSX.Element {
  const openHandle = (): void => window.electron.ipcRenderer.send('open_file')
  const createHandle = (): void => window.electron.ipcRenderer.send('create_file')

  return (
    <>
      <img alt="logo" className="logo" src={electronLogo} />
      <div className="creator">Powered by electron-vite</div>
      <div className="text">
        Build an Electron app with <span className="react">React</span>
        &nbsp;and <span className="ts">TypeScript</span>
      </div>
      <p className="tip">
        Please try pressing <code>F12</code> to open the devTool
      </p>
      <div className="actions">
        <div className="action">
          <a target="_blank" rel="noreferrer" onClick={openHandle}>
            Open File
          </a>
        </div>
        <div className="action">
          <a target="_blank" rel="noreferrer" onClick={createHandle}>
            Create New
          </a>
        </div>
      </div>
      <Versions></Versions>
    </>
  )
}

export default App
