/**
 * This is not a production server yet!
 * This is only a minimal backend to get started.
 */

import * as express from "express";
import * as path from "path";
import * as cors from "cors";
import * as bodyParser from "body-parser";
import * as  sqlite3 from "sqlite3";
import { Project, Pbi } from "@dude/pbi-shared";

const db = new sqlite3.Database("./Times.db", sqlite3.OPEN_CREATE | sqlite3.OPEN_READWRITE, (err) => {
  if (err) {
    return console.error(err.message);
  }
  console.log("Connected to the SQlite database.");
});

// db.run("DROP TABLE pbi");
// db.run(`create table if not exists pbi(id integer not null primary key, name text, project text)`);
// db.run(`create table if not exists project(projectid text not null primary key, name text)`);
const app = express();
app.use(cors());
app.use(bodyParser.json());

app.use("/assets", express.static(path.join(__dirname, "assets")));

app.get("/api/pbi", (req, res) => {
  db.all(`SELECT * FROM main.pbi`, (err, rows: Pbi[]) => {
    if (err) {
      console.error(err.message);
    } else {
      res.send(rows);
    }
  });
});

app.delete("/api/pbi/:id", (req, res) => {
  db.run(`DELETE FROM main.pbi WHERE id = ?`, [req.params.id], (err) => {
    if (err) {
      console.error(err.message);
    } else {
      res.send("ok");
    }
  });
});

app.post("/api/pbi", (req, res) => {
  const pbi: Pbi = req.body;
  console.log("Body", pbi);
  const command = `INSERT INTO pbi(name, project) VALUES('${pbi.name}', '${pbi.project}')`;
  console.debug(command);
  db.run(command);
  db.get(`SELECT * FROM pbi WHERE id = last_insert_rowid()`, (err, row: Pbi) => {
    res.send(row);
  });
});

app.get("/api/project", (req, res) => {
  db.all(`SELECT * FROM main.project`, (err, rows: Project[]) => {
    if (err) {
      console.error(err.message);
    } else {
      res.send(rows);
    }
  });
});

app.get("/api/project/:projectId", (req, res) => {
  const projectId = req.params.projectId;
  db.get(`SELECT * FROM main.project WHERE projectId = '${projectId}'`, (err, rows: Project[]) => {
    if (err) {
      console.error(err.message);

    } else {
      res.send(rows);
    }
  });
});

const port = process.env.port || 3333;
const server = app.listen(port, () => {
  console.log(`Listening at http://localhost:${port}/api`);
});
server.on("error", console.error);

