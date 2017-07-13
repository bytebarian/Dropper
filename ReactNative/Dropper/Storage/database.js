'use strict';

import {manager, ReactCBLite} from 'react-native-couchbase-lite';

ReactCBLite.init(5984, 'admin', 'pass');

var database = new manager('http://admin:pass@localhost:5984/', 'myapp');

module.exports = database;